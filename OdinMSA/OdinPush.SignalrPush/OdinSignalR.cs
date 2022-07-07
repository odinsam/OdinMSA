using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OdinModels.Core.ResultModels;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using OdinPush.SignalrPush.SignalRServices;

namespace OdinPush.SignalrPush;

public interface IOdinSignalRClient
{
    Task Connected(string message);
    Task DisConnected(string message);
    Task SendMessage(string message);
}

public class OdinSignalR:Hub<IOdinSignalRClient>
{
    private readonly ISignalREventMonitorService _eventMonitorService;
    private readonly IOdinLogs _odinLogs;
    public OdinSignalR(ISignalREventMonitorService eventMonitorService,IOdinLogs odinLogs)
    {
        this._eventMonitorService = eventMonitorService;
        this._odinLogs = odinLogs;
    }
    public override Task OnConnectedAsync()
    {
        Clients.Client(this.Context.ConnectionId).Connected(
                JsonConvert.SerializeObject(new ApiResult
                {
                    Message = $"{this.Context.ConnectionId} 已连接",
                    Code = 0,
                })); 
        _eventMonitorService.Connected(this.Context.ConnectionId);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"客户端 {this.Context.ConnectionId} 已连接",
        });
        return base.OnConnectedAsync(); 
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Clients.Client(this.Context.ConnectionId).DisConnected(
                JsonConvert.SerializeObject(new ApiResult
                {
                    Message = $"{this.Context.ConnectionId} 已连接",
                    Code = 0
                })); 
        _eventMonitorService.Disconnected(this.Context.ConnectionId);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"客户端 {this.Context.ConnectionId} 已断开",
        });
        return base.OnDisconnectedAsync(exception);
    }
    
    /// <summary>
    /// server send message to user(one)
    /// </summary>
    /// <param name="receiveConnectionId">接收消息连接Id</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUser(string receiveConnectionId, string message)
    {
        Clients.Client(receiveConnectionId).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToUser(this.Context.ConnectionId,receiveConnectionId,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToUser:\r\n {message} ",
        });
    }
    
    /// <summary>
    /// server send message to users
    /// </summary>
    /// <param name="receiveConnectionIds">接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUsers(List<string> receiveConnectionIds, string message)
    {
        this.Clients.Clients(receiveConnectionIds).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToUsers(this.Context.ConnectionId,receiveConnectionIds,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToUsers:\r\n {message} ",
        });
    }
    
    /// <summary>
    /// server send message  to exceptUser
    /// </summary>
    /// <param name="exceptReceiveConnectionIds">不接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToUsersExcept(List<string> exceptReceiveConnectionIds, string message)
    {
        this.Clients.AllExcept(exceptReceiveConnectionIds).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToExceptUsers(this.Context.ConnectionId,exceptReceiveConnectionIds,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToUsersExcept:\r\n {message} ",
        });
    }
    
    /// <summary>
    /// server send message to group
    /// </summary>
    /// <param name="groupName">接收消息连接群组名</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroup(string groupName, string message)
    {
        
        this.Clients.Group(groupName).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToGroup(this.Context.ConnectionId,groupName,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToGroup:\r\n {message} ",
        });
    }
    
    /// <summary>
    /// server send message to groups
    /// </summary>
    /// <param name="groups">接收消息连接群组集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroups(List<string> groups, string message)
    {
        this.Clients.Groups(groups).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToGroups(this.Context.ConnectionId,groups,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToGroups:\r\n {message} ",
        });
    }
    
    /// <summary>
    /// server send message to group but except some user
    /// </summary>
    /// <param name="groupName">收消息的连接群组</param>
    /// <param name="exceptReceiveConnectionIds">不接收消息的连接Id集合</param>
    /// <param name="message">发送信息</param>
    public void ServerSendMessageToGroupsExceptUsers(string groupName,List<string> exceptReceiveConnectionIds, string message)
    {
        this.Clients.GroupExcept(groupName,exceptReceiveConnectionIds).SendMessage(
            JsonConvert.SerializeObject(new ApiResult<string>
            {
                Data = message,
                Message ="",
                Code = 0
            }));
        _eventMonitorService.SendMessageToGroupsExceptUsers(this.Context.ConnectionId,groupName,exceptReceiveConnectionIds,message);
        _odinLogs.Info(new LogInfo()
        {
            LogContent = $"ServerSendMessageToGroupsExceptUsers:\r\n {message} ",
        });
    }
}