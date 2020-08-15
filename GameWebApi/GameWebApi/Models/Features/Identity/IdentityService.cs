using GameWebApi.Models.DB;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration _configuration;
        public IdentityService(IConfiguration config,GameDBContext ctx)
        {
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
            var z1 = Type.GetType("Int");
            var z2 = Type.GetType("System.Int32");
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

        public void Login()
        {

        }

        public async Task<IEnumerable<PlayerIdentity>> Register()
        {
           return await _context.PlayerIdentity.Include(dates => dates.PlayerDates).ToListAsync();
        }
    }
}
