using OdinMSA.OdinLog.Core.Models;

namespace OdinMSA.OdinLog.Core.LogFactory
{
    public abstract class AbsLogException : AbsOdinLogException
    {
        protected AbsLogException(EnumLogLevel logLevel, LogConfig config) : base(logLevel, config)
        {
        }

        private LogResponse WriteLog(string logContent) => null;
    }
}

