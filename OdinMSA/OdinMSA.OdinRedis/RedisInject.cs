using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OdinModels.OdinRedis;

namespace OdinMSA.OdinRedis;

public static class RedisInject
{
    #region RestTemplate

    /// <summary>
    /// builder AddSingleton redis
    /// </summary>
    /// <param name="services">services</param>
    /// <param name="config">config</param>
    /// <returns></returns>
    public static void AddSingletonMsaRedis(this IServiceCollection services,IConfiguration config)
    {
        RedisConfigInfo.Config = config;
        services.AddSingleton<RedisStringService>();
        services.AddSingleton<RedisEventService>();
        services.AddSingleton<RedisHashService>();
        services.AddSingleton<RedisListService>();
        services.AddSingleton<RedisSetService>();
        services.AddSingleton<RedisZSetService>();
    }
    
    /// <summary>
    /// builder AddSingleton redis
    /// </summary>
    /// <param name="services">builder</param>
    /// <param name="config">config</param>
    /// <returns></returns>
    public static void AddTransientMsaRedis(this IServiceCollection services,IConfiguration config)
    {
        RedisConfigInfo.Config = config;
        services.AddTransient<RedisStringService>();
        services.AddTransient<RedisEventService>();
        services.AddTransient<RedisBase, RedisHashService>();
        services.AddTransient<RedisListService>();
        services.AddTransient<RedisSetService>();
        services.AddTransient<RedisZSetService>();
    }

    /// <summary>
    /// builder AddSingleton redis
    /// </summary>
    /// <param name="services">builder</param>
    /// <param name="config">config</param>
    /// <returns></returns>
    public static void AddScopedMsaRedis(this IServiceCollection services,IConfiguration config)
    {
        RedisConfigInfo.Config = config;
        services.AddScoped<RedisStringService>();
        services.AddScoped<RedisEventService>();
        services.AddScoped<RedisHashService>();
        services.AddScoped<RedisListService>();
        services.AddScoped<RedisSetService>();
        services.AddScoped<RedisZSetService>();
    }
    #endregion
}