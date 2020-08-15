using GameWebApi.Models.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Models.Features.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly GameDBContext _context;
        private IConfiguration _configuration;
        private readonly IOptions<ApplicationSettings> _applicationSettings;
        public IdentityService(IConfiguration config,
            GameDBContext ctx,
            IOptions<ApplicationSettings> applicationSettings)
        {
            _applicationSettings = applicationSettings;
            _configuration = config;
            _context = ctx;
        }


        public void Login1()
        {
            var z = _context.PlayerIdentity.Include(dates => dates.PlayerDates).ToList();
            //var zz = _context.Set<ValidationResult>().FromSql("Common.CheckIdentityValid").FirstOrDefaultAsync();
            //var zzz = _context.Database.ExecuteSqlRaw("Common.CheckIdentityValid");
            // var zzzz = _context.Database.SqlQuery<bool>("Common.CheckIdentityValid").ToList();

            // var connection = (SqlConnection)_context.Database.AsSqlServer().Connection.DbConnection;
            var connection = (SqlConnection)_context.Database.GetDbConnection();
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Common.CheckIdentityValid";
            var reader = command.ExecuteReader();
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
        }

        public async Task<UserLoginResponse> Login(UserInfo userInfo)
        {
            var z = _applicationSettings;

            if (userInfo.Login != null && userInfo.Password != null)
            {
                var user = await GetUser(userInfo.Login, userInfo.Password);
                if (user != null)
                    return new UserLoginResponse{ PlayerId = user.Id, PlayerNickName = user.Nick};
            }
            return new UserLoginResponse { PlayerId = -1, PlayerNickName = "unknown" }; ;
        }

        private async Task<PlayerIdentity> GetUser(string login, string password)
        {
            return await _context.PlayerIdentity.FirstOrDefaultAsync(t => t.Login == login && t.Password == password);
        }


        public async Task<IEnumerable<PlayerIdentity>> Register()
        {
          // return await _context.PlayerIdentity.Include(dates => dates.PlayerDates).ToListAsync();
           return await _context.PlayerIdentity.ToListAsync();
        }
    }
}
