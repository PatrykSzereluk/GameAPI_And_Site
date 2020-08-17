using GameWebApi.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GameWebApi.Managers.Interfaces;
using GameWebApi.Models.Features.Identity;
using GameWebApi.Services.Interfaces;

//using GameWebApi.Models.Features.Identity;

namespace GameWebApi.Services
{
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
            //var z = _context.PlayerIdentity.Include(dates => dates.PlayerDates).ToList();
            //var zz = _context.Set<ValidationResult>().FromSql("Common.CheckIdentityValid").FirstOrDefaultAsync();
            //var zzz = _context.Database.ExecuteSqlRaw("Common.CheckIdentityValid");
            // var zzzz = _context.Database.SqlQuery<bool>("Common.CheckIdentityValid").ToList();

            // var connection = (SqlConnection)_context.Database.AsSqlServer().Connection.DbConnection;

            var connection = (SqlConnection)_context.Database.GetDbConnection();
            //using (SqlCommand cmd = new SqlCommand("SELECT * FROM Common.PlayerIdentity",connection))
            //{
            //    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            //    adapt.SelectCommand.CommandType = CommandType.Text;
            //    DataTable dt = new DataTable();
            //    adapt.Fill(dt);
            //    foreach (var row in dt.Rows)
            //    {
            //        var z = ((DataRow)row).ItemArray;
            //    }
            //}
            _sqlManager.ExecuteDataCommand("asd",CommandType.StoredProcedure);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "SELECT * FROM Common.PlayerIdentity";
            using (var reader = command.ExecuteReader())
            {
                do { 
                    var columnNames = reader.GetColumnSchema().Select(t => t.ColumnName).ToList();

                    var columnNamesd = reader.GetColumnSchema().Select(t => new { Type = t.DataType.FullName, Value = t.ColumnName }).ToList();

                    var cs = reader.GetColumnSchema();
                    while (reader.Read())
                    {
                        int i = 0;
                        foreach (string columnName in columnNames)
                        {
                            var tmp = reader[columnName];
                            var tmp2 = Convert.ChangeType(reader[columnNamesd[i].Value], Type.GetType(columnNamesd[i].Type));
                            i++;
                        }
                    }
                } while (reader.NextResult());
            }
            

            connection.Close();
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
