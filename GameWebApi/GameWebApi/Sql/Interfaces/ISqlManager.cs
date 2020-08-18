

namespace GameWebApi.Sql.Interfaces
{
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Models;

    public interface ISqlManager
    {
        Task<TableSets> ExecuteDataCommand(string command, CommandType commandType, int? timeout = null,
            params SqlParameter[] parameters);
    }
}
