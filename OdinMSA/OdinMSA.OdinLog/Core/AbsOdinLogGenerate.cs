using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinPush.Core;

namespace OdinMSA.OdinLog.Core
{
    public abstract class AbsOdinLogGenerate : IOdinLog
    {
        protected LogConfig _config;
        public abstract LogModel GenerateLog(EnumLogLevel logLevel,LogInfo log);
    }
}