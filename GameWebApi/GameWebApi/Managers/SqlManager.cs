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
        public async Task<int> ExecuteDataCommand(string command, CommandType commandType, int? timeout = null, params SqlParameter[] parameters)
        {

            SqlConnection connection = null;

            try
            {
                connection = new SqlConnection(GetSqlConnection());
                await connection.OpenAsync();
                using (var sqlCommand = connection.CreateCommand())
                {
                    sqlCommand.CommandText = command;
                    sqlCommand.CommandType = commandType;
                    sqlCommand.Parameters.AddRange(parameters);
                    sqlCommand.CommandTimeout = 3000;

                    if (timeout.HasValue)
                    {
                        sqlCommand.CommandTimeout = timeout.Value;
                    }

                    using (var sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        // Add new table
                        do
                        {
                            var columSchema = sqlReader.GetColumnSchema();
                            if (columSchema == null) throw new Exception("Table schema does not contain data");
                            var columns = columSchema.Select(t => new { Type = t.DataType.FullName, Value = t.ColumnName }).ToList();

                            while (sqlReader.Read()) // Read the row
                            {
                                foreach (var column in columns) // Read single value
                                {
                                    var z = sqlReader[column.Value];
                                }
                            }


                        } while (sqlReader.NextResultAsync().Result); // Get next table
                    }



                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                connection?.CloseAsync();
            }

            return 6;

        }
    }
}
