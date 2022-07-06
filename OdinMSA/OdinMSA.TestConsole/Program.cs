// See https://aka.ms/new-console-template for more information

using System.Threading.Channels;
using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("Hello, World!");
var conn = CreateHubConnection();
await conn.StartAsync();

Console.ReadLine();





static HubConnection CreateHubConnection()
{
    var connection = new HubConnectionBuilder()
                        .WithUrl("http://localhost:5251/OdinSignalR")
                        .WithAutomaticReconnect()
                        .Build();
    connection.On<string>("ServerSendMessageToUser",(message) =>
    {
        Console.WriteLine($"服务器发送信息：{message}");
    });
    return connection;
}