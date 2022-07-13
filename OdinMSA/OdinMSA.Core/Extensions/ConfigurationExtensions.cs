using System;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OdinModels.OdinUtils.OdinExtensions;
namespace OdinMSA.Core.Extensions;

public static class ConfigurationExtensions
{
    public static T GetSectionModel<T>(this IConfiguration configuration,string configPath)
    {
        return configuration.GetSection(configPath).Get<T>();
    }
    
    public static T GetSectionValue<T>(this IConfiguration configuration,string configPath)
    {
        var value = configuration.GetSection(configPath).Value;
        return (T)Convert.ChangeType(value,typeof(T));
    }
}