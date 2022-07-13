using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdinModels.Constants;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.Core.Extensions;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using SqlSugar;

namespace OdinMSA.OdinLog
{
    public static class OdinLogInject
    {
        public static IServiceCollection AddOdinSingletonOdinLogs(
            this IServiceCollection services,IConfiguration config)
        {
            var logConfig = config.GetSectionModel<LogConfig>(SystemConstant.KEY_OF_ODINLOGS_SECTION);
            logConfig.ConnectionString = config.GetSectionValue<string>(SystemConstant.KEY_OF_CONNECTIONSTRING_SECTION);
            logConfig.DataBaseType = 
                config.GetSectionValue<string>(SystemConstant.KEY_OF_ODINLOGS_DBTYPE_SECTION)
                .ConvertStringToEnum<DbType>();
            var saveType = config.GetSectionValue<string>(SystemConstant.KEY_OF_ODINLOGS_SAVETYPE_SECTION);
            List<EnumLogSaveType> saveTypes = new List<EnumLogSaveType>();
            foreach (var st in saveType.Split(","))
            {
                saveTypes.Add(st.ConvertStringToEnum<EnumLogSaveType>());
            }
            if(saveTypes.Count>0)
                logConfig.LogSaveType = saveTypes.ToArray();
            var opts = new OdinLogOption() { Config = logConfig };
            services.AddSingleton<IOdinLogs>(provider => new OdinLogs(opts));
            System.Console.WriteLine($"注入类型【 OdinLogs 】");
            return services;
        }
        public static IServiceCollection AddOdinSingletonOdinLogs(
            this IServiceCollection services, 
            Action<OdinLogOption> action)
        {
            var opts = new OdinLogOption();
            action(opts);
            services.AddSingleton<IOdinLogs>(provider => new OdinLogs(opts));
            System.Console.WriteLine($"注入类型【 OdinLogs 】");
            return services;
        }
        
        public static IServiceCollection AddTransientOdinLogs(
            this IServiceCollection services, 
            Action<OdinLogOption> action)
        {
            var opts = new OdinLogOption();
            action(opts);
            services.AddTransient<IOdinLogs>(provider => new OdinLogs(opts));
            System.Console.WriteLine($"注入类型【 OdinLogs 】");
            return services;
        }
        
        public static IServiceCollection AddScopedOdinLogs(
            this IServiceCollection services, 
            Action<OdinLogOption> action)
        {
            var opts = new OdinLogOption();
            action(opts);
            services.AddScoped<IOdinLogs>(provider => new OdinLogs(opts));
            System.Console.WriteLine($"注入类型【 OdinLogs 】");
            return services;
        }
    }
}