using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace GameWebApi.Sql.Helpers
{
    public static class SqlParameters
    {
        public static SqlParameter ToSqlParameter(this int value, string parameterName)
        {
            return new SqlParameter(parameterName, SqlDbType.Int) {Value = value};
        }

        public static SqlParameter ToSqlParameter(this bool value, string parameterName)
        {
            return new SqlParameter(parameterName, SqlDbType.Bit) { Value = value };
        }

        public static SqlParameter ToSqlParameter(this string value, string parameterName, bool isVarChar = false)
        {

            var sqlType = isVarChar ? SqlDbType.VarChar : SqlDbType.NVarChar;

            return new SqlParameter(parameterName, sqlType) { Value = value };
        }

    }
}
