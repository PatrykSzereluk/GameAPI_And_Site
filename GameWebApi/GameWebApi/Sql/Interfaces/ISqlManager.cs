namespace GameWebApi.Sql.Interfaces
{
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;

    public interface ISqlManager
    {
        Task<int> ExecuteDataCommand(string command, CommandType commandType, int? timeout = null,
            params SqlParameter[] parameters);
    }
}
