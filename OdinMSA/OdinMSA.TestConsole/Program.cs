// See https://aka.ms/new-console-template for more information
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using OdinModels.Core.ResultModels;
using OdinModels.OdinUtils.OdinExtensions;

Console.WriteLine("Hello, World!");
var conn = CreateHubConnection();
await conn.StartAsync();
while (true)
{
    string? connId = Console.ReadLine();
    string? message = Console.ReadLine();
    await conn.SendAsync("ServerSendMessageToUser", connId, message);
    Thread.Sleep(1000);
}

static HubConnection CreateHubConnection()
{
    var connection = new HubConnectionBuilder()
                        .WithUrl("http://localhost:5251/OdinSignalR")
                        .WithAutomaticReconnect()
                        .Build();
    connection.On<string>("Connected",(message) =>
    {
        Console.WriteLine($"服务器发送信息");
        var result = GetSignalRResult(message);
        Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
    });
    connection.On<string>("DisConnected",(message) =>
    {
        Console.WriteLine($"服务器发送信息");
        var result = GetSignalRResult(message);
        Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
    });
    connection.On<string>("SendMessage",(message) =>
    {
        Console.WriteLine($"服务器发送信息");
        var result = GetSignalRResult(message);
        Console.WriteLine(JsonConvert.SerializeObject(result).ToJsonFormatString());
    });
    return connection;
}

static ApiResult<string>? GetSignalRResult(string message)=>JsonConvert.DeserializeObject<ApiResult<string>>(message);