namespace GameWebApi.Services
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
    using Models.Features.Identity;
    using Interfaces;
    using GameWebApi.Sql.Interfaces;

    public class IdentityService : IIdentityService
    {
        private readonly GameDBContext _context;
        private IConfiguration _configuration;
        private readonly ApplicationSettings _applicationSettings;
        private readonly ISqlManager _sqlManager;
        public IdentityService(IConfiguration config,
            GameDBContext ctx,
            IOptions<ApplicationSettings> applicationSettings,
            ISqlManager sqlManager)
        {
            _applicationSettings = applicationSettings.Value;
            _configuration = config;
            _context = ctx;
            _sqlManager = sqlManager;
        }


        public void Login1()
        {

           var z = _sqlManager.ExecuteDataCommand("SELECT * FROM Common.PlayerIdentity", CommandType.Text).Result;
           
        }

        public async Task<UserLoginResponse> Login(UserInfo userInfo)
        {

            if (userInfo.Login == null && userInfo.Password == null)
            {
                return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" };
            }

            var user = await GetUser(userInfo.Login, userInfo.Password);
          
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

            return new UserLoginResponse { PlayerId = user.Id, PlayerNickName = user.Nick, Token = encryptToken };
        }

        private async Task<PlayerIdentity> GetUser(string login, string password)
        {
            return await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login && t.Password == password);
        }


        public async Task<IEnumerable<PlayerIdentity>> Register(RegisterRequestModel newPlayer)
        {
           
          // return await _context.PlayerIdentity.Include(dates => dates.PlayerDates).ToListAsync();
          return await _context.PlayerIdentity.ToListAsync();
        }
    }
}
