

namespace GameWebApi.Sql.Managers
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.Data.SqlClient;
    using Microsoft.Extensions.Configuration;
    using Models;

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
        public async Task<TableSets> ExecuteDataCommand(string command, CommandType commandType, int? timeout = null, params SqlParameter[] parameters)
        {
            SqlConnection connection = null;
            TableSets tableSets = new TableSets();

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
                        do
                        {
                            TableSet tableSet = new TableSet();

                            var columnSchema = sqlReader.GetColumnSchema();
                            if (columnSchema == null) throw new Exception("Table schema does not contain data");
                            var columns = columnSchema.Select(t => new { Type = t.DataType.FullName, Value = t.ColumnName }).ToDictionary(t => t.Value, v => v.Type);

                            tableSet.SetTableHeader(columns);

                            while (sqlReader.ReadAsync().Result) // Read the row
                            {
                                Row row = new Row();
                                foreach (var column in columns) // Read single value
                                {
                                    var item = sqlReader.GetFieldValueAsync<object>(sqlReader.GetOrdinal(column.Key)).Result;
                                    row.Elements.Add(item);
                                }
                                tableSet.Rows.Add(row);
                            }
                            tableSets.Elements.Add(tableSet);

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

            return tableSets;
        }
    }
}
