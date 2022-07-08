using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using OdinModels.Constants;
using OdinModels.OdinConsul;
using OdinMSA.SnowFlake;

namespace OdinMSA.OdinConsul.Extensions;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder RegisterConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime, ServiceEntity serviceEntity)
    {
        var consulPort = serviceEntity.ConsulPort != 80 ? $":{serviceEntity.ConsulPort}" : "";
        var consulClient = new ConsulClient(
            //请求注册的 Consul 地址
            x => x.Address = new Uri($"{serviceEntity.ConsulProtocol}://{serviceEntity.ConsulIP}{consulPort}"));
        var httpCheck = new AgentServiceCheck()
        {
            //服务启动多久后注册
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(serviceEntity.DeregisterCriticalServiceAfter),
            //健康检查时间间隔，或者称为心跳间隔
            Interval = TimeSpan.FromSeconds(serviceEntity.HealInterval),
            //健康检查地址
            HTTP = $"{serviceEntity.ServerProtocol}://{serviceEntity.ServerIP}:{serviceEntity.ServerPort}{SystemConstant.KEY_OF_HEALTH_CHECK_PATH}",
            Timeout = TimeSpan.FromSeconds(serviceEntity.TimeOut)
        };
        var snowFlake = (IOdinSnowFlake)app.ApplicationServices.GetService(typeof(IOdinSnowFlake));
        // Register service with consul
        var registration = new AgentServiceRegistration()
        {
            Checks = new[] { httpCheck },
            ID = snowFlake!=null? snowFlake.CreateSnowFlakeId().ToString() :Guid.NewGuid().ToString(),
            Name = serviceEntity.ServiceName,
            Address = serviceEntity.ServerIP,
            Port = serviceEntity.ServerPort,
            //添加 tag 标签，以便 Fabio 识别
            Tags = serviceEntity.Tags
        };

        consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
        lifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
        });

        return app;
    }
}