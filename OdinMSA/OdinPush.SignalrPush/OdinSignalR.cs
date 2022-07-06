using Microsoft.AspNetCore.SignalR;
using OdinPush.SignalrPush.SignalRServices;

namespace OdinPush.SignalrPush;

public class OdinSignalR:Hub
{
    private  readonly ISignalREventMonitorService _eventMonitorService;
    public OdinSignalR(ISignalREventMonitorService eventMonitorService)
    {
        this._eventMonitorService = eventMonitorService;
    }
    public override Task OnConnectedAsync()
    {
        _eventMonitorService.Connected(this.Context.ConnectionId);
        Console.WriteLine($"客户端 {this.Context.ConnectionId} 已连接");
        return base.OnConnectedAsync(); 
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _eventMonitorService.Disconnected(this.Context.ConnectionId);
        Console.WriteLine($"客户端 {this.Context.ConnectionId} 已断开");
        return base.OnDisconnectedAsync(exception);
    }
    
    /// <summary>
    /// server send message to user(one)
    /// </summary>
    /// <param name="receiveConnectionId">接收消息连接Id</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUser(string receiveConnectionId, string message)
    {
        this.Clients.Client(receiveConnectionId).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToUser");
        _eventMonitorService.SendMessageToUser(this.Context.ConnectionId,receiveConnectionId,message);
    }
    
    /// <summary>
    /// server send message to users
    /// </summary>
    /// <param name="receiveConnectionIds">接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUsers(List<string> receiveConnectionIds, string message)
    {
        this.Clients.Clients(receiveConnectionIds).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToUsers");
        _eventMonitorService.SendMessageToUsers(this.Context.ConnectionId,receiveConnectionIds,message);
    }
    
    /// <summary>
    /// server send message  to exceptUser
    /// </summary>
    /// <param name="exceptReceiveConnectionIds">不接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUsersExcept(List<string> exceptReceiveConnectionIds, string message)
    {
        this.Clients.AllExcept(exceptReceiveConnectionIds).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToUsersExcept");
        _eventMonitorService.SendMessageToExceptUsers(this.Context.ConnectionId,exceptReceiveConnectionIds,message);
    }
    
    /// <summary>
    /// server send message to group
    /// </summary>
    /// <param name="groupName">接收消息连接群组名</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroup(string groupName, string message)
    {
        this.Clients.Group(groupName).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToGroup");
        _eventMonitorService.SendMessageToGroup(this.Context.ConnectionId,groupName,message);
    }
    
    /// <summary>
    /// server send message to groups
    /// </summary>
    /// <param name="groups">接收消息连接群组集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroups(List<string> groups, string message)
    {
        this.Clients.Groups(groups).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToGroups");
        _eventMonitorService.SendMessageToGroups(this.Context.ConnectionId,groups,message);
    }
    
    /// <summary>
    /// server send message to group but except some user
    /// </summary>
    /// <param name="groupName">收消息的连接群组</param>
    /// <param name="exceptReceiveConnectionIds">不接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroupsExceptUsers(string groupName,List<string> exceptReceiveConnectionIds, string message)
    {
        this.Clients.GroupExcept(groupName,exceptReceiveConnectionIds).SendAsync("SendMessage",message);
        Console.WriteLine("ServerSendMessageToGroupsExceptUsers");
        _eventMonitorService.SendMessageToGroupsExceptUsers(this.Context.ConnectionId,groupName,exceptReceiveConnectionIds,message);
    }
}