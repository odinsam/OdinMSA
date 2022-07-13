// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OdinModels.Core.ResultModels;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.OdinRedis;

#region redis 订阅发布
var builder = new ConfigurationBuilder();
builder.AddJsonFile("config.json");
var configuration = builder.Build();
IServiceCollection service = new ServiceCollection();
service.AddSingletonMsaRedis(configuration);
var redisEvent = new RedisEventService(null);
string rl = Console.ReadLine();
if (rl == "1")
{
    redisEvent.Subscribe("odinChannel",((s, s1, subScription) =>
    {
        Console.WriteLine($"client:{s}");
        Console.WriteLine($"client:{s1}");
    } ));
}
if (rl == "2")
{
    redisEvent.Publish("odinChannel","publish message");
}


Console.WriteLine("over");
#endregion

#region signalR DEMO
// Console.WriteLine("Hello, World!");
// var conn = CreateHubConnection();
// await conn.StartAsync();
// while (true)
// {
//     string? connId = Console.ReadLine();
//     string? message = Console.ReadLine();
//     await conn.SendAsync("ServerSendMessageToUser", connId, message,true);
//     Thread.Sleep(1000);
// }
//
// static HubConnection CreateHubConnection()
// {
//     var connection = new HubConnectionBuilder()
//         .WithUrl("http://localhost:5251/OdinSignalR")
//         .WithAutomaticReconnect()
//         .Build();
//     connection.On<string>("Connected",(message) =>
//     {
//         Console.WriteLine($"服务器发送信息");
//         var result = GetSignalRResult(message);
//         Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
//     });
//     connection.On<string>("DisConnected",(message) =>
//     {
//         Console.WriteLine($"服务器发送信息");
//         var result = GetSignalRResult(message);
//         Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
//     });
//     connection.On<string>("SendMessage",(message) =>
//     {
//         Console.WriteLine($"服务器发送信息");
//         var result = GetSignalRResult(message);
//         Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
//     });
//     return connection;
// }
//
// static ApiResult<string>? GetSignalRResult(string message)=>JsonConvert.DeserializeObject<ApiResult<string>>(message);


#endregion
