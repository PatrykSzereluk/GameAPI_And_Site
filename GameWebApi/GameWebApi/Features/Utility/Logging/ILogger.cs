using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebApi.Features.Utility.Logging
{
    public interface ILogger
    {
        void Info(string message, string args = null);
        void Debug(string message, string args = null);
        void Warning(string message, string args = null);
        void Error(string message, string args = null);
    }
}
