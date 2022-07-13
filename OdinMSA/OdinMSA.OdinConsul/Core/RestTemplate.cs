using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using OdinModels.OdinUtils.OdinExceptionExtensions;
using OdinModels.OdinUtils.OdinExtensions;
using OdinModels.OdinUtils.Utils.OdinAlgorithm;
using OdinMSA.Core;
using OdinMSA.Core.Core.Interface;
using OdinMSA.OdinConsul.Core.Interface;

namespace OdinMSA.OdinConsul.Core;

public class RestTemplate : IRestTemplate
{
    private readonly IMsaWebApi _msaWebApi;
    private readonly IConfiguration _configuration;
    public RestTemplate(IMsaWebApi msaWebApi,IConfiguration configuration)
    {
        this._msaWebApi = msaWebApi;
        this._configuration = configuration;
    }
    
    /// <summary>
    /// 把http://consulServiceName/api/values转换为http://consulServiceUrl/api/values
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public async Task<String> ResolveUrlAsync(String url)
    {
        Uri uri = new Uri(url);
        String serviceName = uri.Host;//apiservice1
        String realRootUrl = await ResolveRootUrlAsync(serviceName);//查询出来apiservice1对应的服务器地址192.168.1.1:5000
        //uri.Scheme=http,realRootUrl =192.168.1.1:5000,PathAndQuery=/api/values
        return uri.Scheme + "://" + realRootUrl + uri.PathAndQuery;
    }
    
    /// <summary>
    /// 获取服务的第一个实现地址
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    private async Task<String> ResolveRootUrlAsync(String serviceName)
    {
        var consulConfig = RegisterConfigHelper.BuilderRegisterConfig(this._configuration).ConsulConfig;
        using (var consulClient = new ConsulClient(
                   c => c.Address = new Uri(consulConfig.GetConsulUrl())))
        {
            var services = (await consulClient.Agent.Services()).Response.Values
                .Where(s => s.Service.Equals(serviceName, StringComparison.OrdinalIgnoreCase));
            if (!services.Any())
                throw new OdinException(OdinMSACoreException.RestTemplateServiceNotFind);
            else
            {
                //根据服务器权重 tags 里的第一个值为权重值
                List<int> serviceWeight = new List<int>();
                foreach (var ser in services)
                    serviceWeight.Add(ser.Tags[0].ToInt());
                var index = RandomHelper.GetRandomByWeight(serviceWeight);
                index = index == -1 ? 0 : index;
                var service = services.ElementAt(index);
                return $"{service.Address}:{service.Port}";
            }
        }
    }
}