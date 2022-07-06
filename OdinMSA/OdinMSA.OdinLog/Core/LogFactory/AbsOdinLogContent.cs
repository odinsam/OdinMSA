using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinPush.Core;

namespace OdinMSA.OdinLog.Core.LogFactory
{
    public abstract class AbsOdinLogContent : AbsOdinLogFace
    {
        protected AbsOdinLogContent(EnumLogLevel logLevel, LogConfig config) : base(logLevel, config)
        {
        }

        public override LogResponse WriteLog(LogInfo log) => null;
    }
}




