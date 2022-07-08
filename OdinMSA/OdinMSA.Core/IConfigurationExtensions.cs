using System;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace OdinMSA.Core;

public static class IConfigurationExtensions
{
    public static T GetSectionValue<T>(this IConfiguration configuration,string configPath, T defaultValue=default(T))
    {
        var value = configuration.GetSection(configPath).Value;
        if (string.IsNullOrEmpty(value))
            return default(T);
        try
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        catch (Exception e)
        {
            return (T)Convert.ChangeType(value,typeof(T));
        }
    }
}