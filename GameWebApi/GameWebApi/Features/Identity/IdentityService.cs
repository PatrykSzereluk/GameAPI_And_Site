namespace GameWebApi.Features.Identity
{
    using Models.DB;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using GameWebApi.Sql.Interfaces;
    using GameWebApi.Models.Features.Identity.Models;
    using System.Linq;
    using Microsoft.Data.SqlClient;
    using Security;
    using GameWebApi.Helpers;

    public class IdentityService : IIdentityService
    {
        private readonly GameDBContext _context;
        private readonly IConfiguration _configuration;
        private readonly ApplicationSettings _applicationSettings;
        private readonly ISqlManager _sqlManager;
        private readonly IEncrypter _encrypter;

        public IdentityService(IConfiguration config,
            GameDBContext ctx,
            IOptions<ApplicationSettings> applicationSettings,
            ISqlManager sqlManager,
            IEncrypter encrypter)
        {
            _applicationSettings = applicationSettings.Value;
            _configuration = config;
            _context = ctx;
            _sqlManager = sqlManager;
            _encrypter = encrypter;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userInfo)
        {

            if (userInfo.Login == null && userInfo.Password == null)
            {
                return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };
            }

            var userId = await GetUserIdByLogin(userInfo.Login);

            if(userId == 0) return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };

            StringBuilder sb = new StringBuilder(_encrypter.Encrypted(userInfo.Password));

            var salt = await GetSalt(userId);
            sb.Append(salt.Salt);

            var user = await GetUser(userInfo.Login, sb.ToString());

            if(user == null) return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._applicationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptToken = tokenHandler.WriteToken(token);

            var lastDatePassMod = await GetLastDateModifiedPassword(userId);

            var ask = (DateTime.Today - lastDatePassMod).TotalDays > 90;

            return new UserLoginResponse { PlayerId = user.Id, PlayerNickName = user.Nick, Token = encryptToken, AskAboutChangePassword = ask };
        }

        private async Task<DateTime> GetLastDateModifiedPassword(int playerId)
        {
            var dates = await _context.PlayerDates.FirstOrDefaultAsync(t => t.PlayerId == playerId);
            return dates.LastPasswordChangeDate;
        }

        private async Task<int> GetUserIdByLogin(string login)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login);
            return user != null ? user.Id : 0;
        }

        private async Task<PlayerSalt> GetSalt(int playerId)
        {
            return await _context.PlayerSalt.FirstAsync(t => t.PlayerId == playerId);
        }

        private async Task<PlayerIdentity> GetUser(string login, string password)
        {
            return await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login && t.Password == password);
        }

        public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel newPlayer)
        {
            UserRegisterResponseModel returnValue = new UserRegisterResponseModel {IsSuccess = true};

            if (_context.PlayerIdentity.AnyAsync(t => t.Login == newPlayer.Login || t.Nick == newPlayer.UserName || t.Email == newPlayer.Email).Result)
            {
                returnValue.IsSuccess = false;
                return returnValue;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder hashPassword = new StringBuilder(_encrypter.Encrypted(newPlayer.Password));
            string generateString = default;
            string salt = _encrypter.Encrypted(generateString.GenerateRandomString(40));
            hashPassword.Append(salt);

            var loginParam = new SqlParameter("Login",SqlDbType.NVarChar){Value = newPlayer.Login};
            var passwordParam = new SqlParameter("Password", SqlDbType.NVarChar){Value = hashPassword.ToString() };
            var nickNameParam = new SqlParameter("NickName", SqlDbType.NVarChar){Value = newPlayer.UserName};
            var emailParam = new SqlParameter("Email", SqlDbType.NVarChar){Value = newPlayer.Email};
            var saltHashParam = new SqlParameter("SaltHash", SqlDbType.NVarChar){Value = salt};

            parameters.Add(loginParam);
            parameters.Add(passwordParam);
            parameters.Add(nickNameParam);
            parameters.Add(emailParam);
            parameters.Add(saltHashParam);

            var dataSet = await _sqlManager.ExecuteDataCommand("[Common].[RegisterNewPlayer]", CommandType.StoredProcedure,null,parameters.ToArray());

            var id = dataSet.Elements.First().Rows.First().Elements.First();
            returnValue.PlayerId = (int)id;
            return returnValue;
        }

    }
}
