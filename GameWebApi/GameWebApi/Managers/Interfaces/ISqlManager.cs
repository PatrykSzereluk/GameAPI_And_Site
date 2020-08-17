using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GameWebApi.Managers.Interfaces
{
    public interface ISqlManager
    {
        void ExecuteDataCommand(string command, CommandType commandType, params SqlParameter[] parameters);
    }
}
