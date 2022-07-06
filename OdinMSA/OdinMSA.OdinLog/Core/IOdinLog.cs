using OdinMSA.OdinLog.Core.Models;

namespace OdinMSA.OdinLog.Core
{
    public interface IOdinLog
    {
        LogModel GenerateLog(EnumLogLevel logLevel, LogInfo log);
    }
}
