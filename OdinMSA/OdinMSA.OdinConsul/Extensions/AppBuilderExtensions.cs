using System;
using System.Collections.Generic;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using OdinModels.Constants;
using OdinModels.Core.ConfigModels;
using OdinModels.OdinConsul;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.Core.Extensions;
using OdinMSA.OdinConsul.Core;
using OdinMSA.SnowFlake;

namespace OdinMSA.OdinConsul.Extensions;

public static class AppBuilderExtensions
{
    public static IApplicationBuilder RegisterConsul(
        this IApplicationBuilder app, 
        IHostApplicationLifetime lifetime, 
        IConfiguration configuration,
        ServerConfig serverConfig 
        )
    {
        var registerConfig = RegisterConfigHelper.BuilderRegisterConfig(configuration);
        var consulConfig = registerConfig.ConsulConfig;
        var serviceConfig = registerConfig.ServiceConfig;
        var consuleName = "odin.Consul";
        var consulClient = new ConsulClient(
            //请求注册的 Consul 地址
            x =>
            {
                x.Address = new Uri(consulConfig.GetConsulUrl());
                x.Datacenter = "dc1";
            });
        var httpCheck = new AgentServiceCheck()
        {
            //服务启动多久后注册
            DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(serviceConfig.DeregisterCriticalServiceAfter),
            //健康检查时间间隔，或者称为心跳间隔
            Interval = TimeSpan.FromSeconds(serviceConfig.HealInterval),
            //健康检查地址
            HTTP = registerConfig.ServiceConfig.HealthUrl,
            Timeout = TimeSpan.FromSeconds(serviceConfig.TimeOut)
        };
        var snowFlake = (IOdinSnowFlake)app.ApplicationServices.GetService(typeof(IOdinSnowFlake));
        var serviceId = snowFlake == null
            ? (serviceConfig.ServiceName +"-"+ Guid.NewGuid().ToString("N"))
            : (serviceConfig.ServiceName +"-"+ snowFlake.CreateSnowFlakeId());
        // Register service with consul
        var uriConfig = serverConfig.GetServiceUrlConfigByProtocol();
        var registration = new AgentServiceRegistration()
        {
            Checks = new[]{ httpCheck },
            ID = serviceId,
            Name = serviceConfig.ServiceName,
            Address = uriConfig.Ip,
            Port = uriConfig.Port.ToInt(),
            //添加 tag 标签，以便 Fabio 识别
            Tags = serviceConfig.Tags
        };

        consulClient.Agent.ServiceRegister(registration).Wait();//服务启动时注册，内部实现其实就是使用 Consul API 进行注册（HttpClient发起）
        lifetime.ApplicationStopping.Register(() =>
        {
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();//服务停止时取消注册
        });

        return app;
    }
}
