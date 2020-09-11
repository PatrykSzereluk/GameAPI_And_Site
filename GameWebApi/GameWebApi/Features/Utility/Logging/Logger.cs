using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace GameWebApi.Features.Utility.Logging
{
    public class Logger :ILogger
    {
        private static Logger _instance;
        private static NLog.Logger _logger;

        private Logger()
        {

        }

        public static Logger GetInstance()
        {
            if(_instance == null)
                _instance = new Logger();

            return _instance;
        }

        private NLog.Logger GetLogger(string logger)
        {
            if (Logger._logger == null)
                Logger._logger = LogManager.GetLogger(logger);

            return Logger._logger;
        }

        public void Info(string message, string args = null)
        {
            if (args == null) GetLogger("loggerRole").Info(message);
            else GetLogger("loggerRole").Info(message,args);
        }

        public void Debug(string message, string args = null)
        {
            if (args == null) GetLogger("loggerRole").Debug(message);
            else GetLogger("loggerRole").Debug(message, args);
        }

        public void Warning(string message, string args = null)
        {
            if (args == null) GetLogger("loggerRole").Warn(message);
            else GetLogger("loggerRole").Warn(message, args);
        }

        public void Error(string message, string args = null)
        {
            if (args == null) GetLogger("loggerRole").Error(message);
            else GetLogger("loggerRole").Error(message, args);
        }
    }
}
