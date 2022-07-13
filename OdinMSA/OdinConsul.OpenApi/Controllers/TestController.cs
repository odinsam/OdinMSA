using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OdinModels.OdinUtils.OdinExceptionExtensions;
using OdinMSA.Core.Core.Interface;
using OdinMSA.OdinConsul.Core;
using OdinMSA.OdinConsul.Core.Interface;
using OdinMSA.OdinRedis;

namespace OdinConsul.OpenApi.Controllers;

[ApiController]
public class TestController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly IMsaWebApi _msaWebApi;
    private readonly IRestTemplate _restTemplate;
    private readonly RedisStringService _redisStringService;
    private readonly RedisEventService _redisEventService;
    public TestController(
        IHttpClientFactory httpClientFactory,
        RedisStringService redisStringService,
        RedisEventService redisEventService,
        IMsaWebApi msaWebApi,
        IConfiguration configuration,
        IRestTemplate restTemplate)
    {
        this._httpClientFactory = httpClientFactory;
        this._msaWebApi = msaWebApi;
        this._configuration = configuration;
        this._restTemplate = restTemplate;
        this._redisStringService = redisStringService;
        this._redisEventService = redisEventService;
    }

    /// <summary>
    /// show api
    /// </summary>
    /// <returns></returns>
    [Route("/api")]
    [HttpGet]
    public string Show()
    {
        this._redisStringService.Set("redis:key1", "value");
        this._redisStringService.Set("redis:key2", "value");
        var str = this._redisStringService.Get("redis:key1");
        this._redisEventService.Publish("odinChannel","web api message");
        return this._restTemplate.ResolveUrlAsync("https://OdinConsul.OpenApi/client").Result+$"key:{str}";
    }
        

    /// <summary>
    /// client
    /// </summary>
    /// <returns></returns>
    [Route("/client")]
    [HttpGet]
    public Model HttpClientTest()
    {
        return this._msaWebApi.Get<Model>("http://127.0.0.1:11080/api");
    }
}

public class Model
{
    public string Name { get; set; }
    public int Age { get; set; }
}