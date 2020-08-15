using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;

namespace SQL
{
    public static class SQLConnector
    {
        public static void GetSQLConnection()
        {
            var conn = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            //var connection = new SqlConnection()
        }

    }
}
