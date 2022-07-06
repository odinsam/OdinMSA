using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinPush.Core;

namespace OdinMSA.OdinLog.Core
{
    public interface IOdinLogs
    {
        LogConfig Config { get; set; }
        
        LogResponse Info(LogInfo log);

        LogResponse Waring(LogInfo log);

        LogResponse Error(ExceptionLog log);
    }
}