using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OdinModels.OdinUtils.OdinExceptionExtensions;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.Core.Core;
using OdinMSA.Core.Core.Interface;
using OdinMSA.Core.Enums;
using OdinMSA.Core.Models;
using Org.BouncyCastle.Crypto.Tls;

namespace OdinMSA.Core.Extensions;

public static class WebApplicationBuilderExtensions
{
    #region inject

    #region WebApi

    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static void AddSingletonMsaWebApi(this IServiceCollection services)
    {
        services.AddSingleton<IMsaWebApi, MsaWebApi>();
    }
    
    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static void AddTransientMsaWebApi(this IServiceCollection services)
    {
        services.AddTransient<IMsaWebApi, MsaWebApi>();
    }

    /// <summary>
    /// builder AddSingleton IMsaWebApi
    /// </summary>
    /// <param name="services">services</param>
    /// <returns></returns>
    public static void AddScopedMsaWebApi(this IServiceCollection services)
    {
        services.AddScoped<IMsaWebApi, MsaWebApi>();
    }
    #endregion

    #endregion
    
    
    /// <summary>
    /// 按照cert创建httpclient
    /// </summary>
    /// <param name="builder">builder</param>
    /// <param name="httpClientInfo">httpClientInfo</param>
    /// <returns></returns>
    /// <exception cref="Exception">httpClientInfo can not be null</exception>
    public static IHttpClientBuilder BuilderAddHttpClient(this WebApplicationBuilder builder, HttpClientInfo httpClientInfo)
    {
        if (httpClientInfo == null)
            throw new OdinException(OdinMSACoreException.BuilderAddHttpClientx01);
        return builder.Services.AddHttpClient(httpClientInfo.ClientName, client =>
        {
            if(!httpClientInfo.BaseAddress.IsNullOrEmpty())
                client.BaseAddress = new Uri(httpClientInfo.BaseAddress);
            if(httpClientInfo.DefaultRequestHeaders!=null && httpClientInfo.DefaultRequestHeaders.Count>0)
                foreach (var defaultRequestHeader in httpClientInfo.DefaultRequestHeaders)
                {
                    client.DefaultRequestHeaders.Add(defaultRequestHeader.Key, defaultRequestHeader.Value);
                }
        }).ConfigurePrimaryHttpMessageHandler(() =>
        {
            var handler = new HttpClientHandler();
            if (httpClientInfo.CertInfos != null && httpClientInfo.CertInfos.Count>0)
            {
                foreach (var cert in httpClientInfo.CertInfos)
                {
                    var clientCertificate = new X509Certificate2(cert.CertPath, cert.CertPassword);
                    handler.ClientCertificates.Add(clientCertificate);
                }
            }
            return handler;
        });
    }
    
    /// <summary>
    /// 服务器加载 Json 文件
    /// </summary>
    /// <param name="builder">builder</param>
    /// <param name="addJsonFiles">JsonFiles</param>
    /// <returns></returns>
    /// <exception cref="Exception">addJsonFiles can not be null and length must greater than 0</exception>
    public static IHostBuilder BuilderAddJsonFile(this WebApplicationBuilder builder, JsonFile[] addJsonFiles)
    {
        if(addJsonFiles==null || addJsonFiles.Length==0)
            throw new OdinException(OdinMSACoreException.BuilderAddJsonFilex01);
        return builder.Host.ConfigureAppConfiguration((context, build) =>
        {
            foreach (var jsonFile in addJsonFiles)  
            {
                build.AddJsonFile(jsonFile.FilePath, optional: jsonFile.Optional, reloadOnChange: jsonFile.ReloadOnChange);
            }
        });
    }
}