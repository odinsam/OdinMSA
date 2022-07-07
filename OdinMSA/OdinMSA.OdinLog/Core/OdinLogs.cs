using System;
using Newtonsoft.Json;
using OdinMSA.OdinLog.Core.LogFactory;
using OdinMSA.OdinLog.Core.Models;

namespace OdinMSA.OdinLog.Core
{
    public class OdinLogs : IOdinLogs
    {
        public LogConfig Config { get; set; }
        public OdinLogs()
        {
        }

        public OdinLogs(LogConfig config)
        {
            this.Config = config;
        }

        public OdinLogs(OdinLogOption opt)
        {
            this.Config = opt.Config;
        }

        public LogResponse Info(LogInfo log)
        {
            var response = OdinLogFactory.GetOdinLogUtils(EnumLogLevel.Info, Config)?.WriteLog(log);
            Console.WriteLine(JsonConvert.SerializeObject(log));
            return response;
        }

        public LogResponse Waring(LogInfo log)
        {
            var response =  OdinLogFactory.GetOdinLogUtils(EnumLogLevel.Waring, Config)?.WriteLog(log);
            Console.WriteLine(JsonConvert.SerializeObject(log));
            return response;

        }

        public LogResponse Error(ExceptionLog log)
        {
            var response =  OdinLogFactory.GetOdinLogUtils(EnumLogLevel.Error, Config)?.WriteLog(log);
            Console.WriteLine(JsonConvert.SerializeObject(log));
            return response;
        }
    }
}