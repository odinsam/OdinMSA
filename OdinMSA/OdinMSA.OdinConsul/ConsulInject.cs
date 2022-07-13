using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OdinMSA.OdinConsul.Core;
using OdinMSA.OdinConsul.Core.Interface;

namespace OdinMSA.OdinConsul;

public static class ConsulInject
{
    #region RestTemplate

    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">builder</param>
    /// <returns></returns>
    public static void AddSingletonMsaRestTemplate(this IServiceCollection services)
    {
        services.AddSingleton<IRestTemplate, RestTemplate>();
    }
    
    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">builder</param>
    /// <returns></returns>
    public static void AddTransientMsaRestTemplate(this IServiceCollection services)
    {
        services.AddTransient<IRestTemplate, RestTemplate>();
    }

    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">builder</param>
    /// <returns></returns>
    public static void AddScopedMsaRestTemplate(this IServiceCollection services)
    {
        services.AddScoped<IRestTemplate, RestTemplate>();
    }
    #endregion
}