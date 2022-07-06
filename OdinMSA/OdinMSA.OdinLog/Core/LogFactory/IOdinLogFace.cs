using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OdinPush.Core;

namespace OdinMSA.OdinLog.Core.LogFactory
{
    public interface IOdinLogFace
    {
        EnumLogLevel LogLevel { get; set; }
        LogConfig Config { get; set; }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="log">log</param>
        LogResponse WriteLog(LogInfo log);
    }
}
