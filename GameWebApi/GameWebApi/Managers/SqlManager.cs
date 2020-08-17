using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GameWebApi.Managers.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace GameWebApi.Managers
{
    public class SqlManager : ISqlManager
    {
        private readonly IConfiguration _configuration;
        public SqlManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetSqlConnection()
        {
           return _configuration.GetConnectionString("DBContext");
        }
        public void ExecuteDataCommand(string command, CommandType commandType, params SqlParameter[] parameters)
        {
            var z = GetSqlConnection();
            SqlConnection connection = new SqlConnection(GetSqlConnection());
        }
    }
}
