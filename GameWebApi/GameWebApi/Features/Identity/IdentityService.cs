namespace GameWebApi.Features.Identity
{
    using GameWebApi.Features.Email.Models;
    using Models;
    using GameWebApi.Models.DB;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Sql.Interfaces;
    using System.Linq;
    using Microsoft.Data.SqlClient;
    using Security;
    using Helpers;
    using GameWebApi.Sql.Helpers;
    using Email;
    using Ban;

    public class IdentityService : IIdentityService
    {
        private readonly GameDBContext _context;
        private readonly ApplicationSettings _applicationSettings;
        private readonly ISqlManager _sqlManager;
        private readonly IEncrypter _encrypter;
        private readonly IBanService _banService;
        private readonly IEmailService _emailService;

        public IdentityService(
            GameDBContext ctx,
            IOptions<ApplicationSettings> applicationSettings,
            ISqlManager sqlManager,
            IEncrypter encrypter,
            IBanService banService,
            IEmailService emailService)
        {
            _applicationSettings = applicationSettings.Value;
            _context = ctx;
            _sqlManager = sqlManager;
            _encrypter = encrypter;
            _banService = banService;
            _emailService = emailService;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userInfo)
        {

            if (userInfo.Login == null && userInfo.Password == null)
            {
                return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };
            }

            var userTuple = await GetUserIdByLogin(userInfo.Login);

            if(userTuple.Item2 == -1) return new UserLoginResponse { PlayerId = -1, PlayerNickName = userTuple.Item1 };

            var isUserBanned = await _banService.CheckUserBan(userTuple.Item2);

            if(isUserBanned) return new UserLoginResponse { PlayerId = userTuple.Item2, PlayerNickName = userTuple.Item1, IsBanned = true};

            StringBuilder sb = new StringBuilder(_encrypter.Encrypted(userInfo.Password));

            var salt = await GetSalt(userTuple.Item2);

            sb.Append(salt.Salt);

            var user = await GetUser(userInfo.Login, sb.ToString());

            if(user == null) return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };

            if(user.EmailConfirmed == false) return new UserLoginResponse(){PlayerNickName = userTuple.Item1, EmailIsNotConfirmed = true};

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

            var lastDatePassMod = await GetLastDateModifiedPassword(userTuple.Item2);

            var ask = (DateTime.Today - lastDatePassMod).TotalDays > _applicationSettings.PasswordChangePeriod;
            // send email notification if is different ip 
            return new UserLoginResponse { PlayerId = user.Id, PlayerNickName = user.Nick, Token = encryptToken, AskAboutChangePassword = ask };
        }

        private async Task<DateTime> GetLastDateModifiedPassword(int playerId)
        {
            var dates = await _context.PlayerDates.FirstOrDefaultAsync(t => t.PlayerId == playerId);
            return dates.LastPasswordChangeDate;
        }

        private async Task<Tuple<string,int>> GetUserIdByLogin(string login)
        {
            var user = await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login);
            return user != null ? Tuple.Create(user.Nick, user.Id) : Tuple.Create("unknown", -1);
        }

        private async Task<PlayerSalt> GetSalt(int playerId)
        {
            return await _context.PlayerSalt.FirstOrDefaultAsync(t => t.PlayerId == playerId);
        }

        private async Task<PlayerIdentity> GetUser(string login, string password)
        {
            return await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login && t.Password == password);
        }

        public async Task<UserRegisterResponseModel> Register(UserRegisterRequestModel newPlayer)
        {
            UserRegisterResponseModel returnValue = new UserRegisterResponseModel {IsSuccess = true};

            if (_context.PlayerIdentity.AnyAsync(t => t.Login == newPlayer.Login || t.Nick == newPlayer.NickName || t.Email == newPlayer.Email).Result)
            {
                returnValue.IsSuccess = false;
                returnValue.StatusCode = 456;
                return returnValue;
            }

            List<SqlParameter> parameters = new List<SqlParameter>();

            StringBuilder hashPassword = new StringBuilder(_encrypter.Encrypted(newPlayer.Password));
            string generateString = default;
            string salt = _encrypter.Encrypted(generateString.GenerateRandomString(40));
            hashPassword.Append(salt);


            var loginParam = newPlayer.Login.ToSqlParameter("Login");
            var passwordParam = hashPassword.ToString().ToSqlParameter("Password");
            var nickNameParam = newPlayer.NickName.ToSqlParameter("NickName");
            var emailParam = newPlayer.Email.ToSqlParameter("Email");
            var saltHashParam = salt.ToSqlParameter("SaltHash");
            var retValParam = true.ToSqlParameter("ReturnValue");
            var playerHashParam = newPlayer.GetHashCode().ToString().ToSqlParameter("PlayerHash");

            parameters.Add(loginParam);
            parameters.Add(passwordParam);
            parameters.Add(nickNameParam);
            parameters.Add(emailParam);
            parameters.Add(saltHashParam);
            parameters.Add(retValParam);
            parameters.Add(playerHashParam);

            var dataSet = await _sqlManager.ExecuteDataCommand("[Common].[RegisterNewPlayer]", CommandType.StoredProcedure,null,parameters.ToArray());

            var id = dataSet.Elements.First().Rows.First().Elements.First();
            returnValue.NickName = newPlayer.NickName;
            returnValue.PlayerId = (int)id;
            await _emailService.SendEmailToUser(newPlayer.Email,"",EmailType.Welcome, new EmailData(){NickName = returnValue.NickName, PlayerId  = returnValue.PlayerId});
            // return error code
            return returnValue;
        }

    }
}
    