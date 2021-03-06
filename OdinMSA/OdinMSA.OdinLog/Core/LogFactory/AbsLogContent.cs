using System;
using OdinMSA.OdinLog.Core.Models;

namespace OdinMSA.OdinLog.Core.LogFactory
{
    public abstract class AbsLogContent : AbsOdinLogContent
    {
        protected AbsLogContent(EnumLogLevel logLevel, LogConfig config) : base(logLevel, config)
        {
        }

        private LogResponse WriteLog(Exception exception,long? logId=null) => null;
    }
}

