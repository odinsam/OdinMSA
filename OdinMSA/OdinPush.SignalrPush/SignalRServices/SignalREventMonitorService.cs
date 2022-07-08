using Newtonsoft.Json;
using OdinModels.OdinSignalR.Entities;
using OdinModels.OdinSignalR.Enums;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.OdinEF;
using OdinMSA.SnowFlake;
using SqlSugar;

namespace OdinPush.SignalrPush.SignalRServices;

public class SignalREventMonitorService : Repository<PushRecordSignalREntity>,ISignalREventMonitorService
{
    private readonly IOdinSnowFlake _odinSnowFlake;
    public SignalREventMonitorService(ISqlSugarClient db,IOdinSnowFlake odinSnowFlake) : base(db)
    {
        this._odinSnowFlake = odinSnowFlake;
    }
    
    public bool Connected(string connectionId,string message)
    {
        return base.Insert(new PushRecordSignalREntity()
        {
            FromUser = connectionId,
            PushContent = message,
            EventType = EnumEventType.SignalrConnected.GetDescription()
        });
    }
    
    public bool Disconnected(string connectionId,string message)
    {
        return base.Insert(new PushRecordSignalREntity()
        {
            FromUser = connectionId,
            PushContent = message,
            EventType = EnumEventType.SignalrDisConnected.GetDescription()
        });
    }

    public bool SendMessageToUser(string sendConnectionId, string receiveConnectionId, string message, bool isClientInvoke = false)
    {
        return base.Insert(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
        });
    }

    public bool SendMessageToUsers(string sendConnectionId, List<string> receiveConnectionIds, string message, bool isClientInvoke = false)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var receiveConnectionId in receiveConnectionIds)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
            });
        }
        return base.InsertRange(entities);
    }

    public bool SendMessageToExceptUsers(string sendConnectionId, List<string> exceptReceiveConnectionIds, string message, bool isClientInvoke = false)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var receiveConnectionId in exceptReceiveConnectionIds)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
                Remark = "ExceptUser",
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
            });
        }
        return base.InsertRange(entities);
    }

    public bool SendMessageToGroup(string sendConnectionId, string groupName, string message, bool isClientInvoke = false)
    {
        return base.Insert(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToGroup = groupName,
                PushContent = message,
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
        });
    }

    public bool SendMessageToGroups(string sendConnectionId, List<string> groups, string message, bool isClientInvoke = false)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var group in groups)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToGroup = group,
                PushContent = message,
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
            });
        }
        return base.InsertRange(entities);
    }

    public bool SendMessageToGroupsExceptUsers(
        string sendConnectionId, 
        string groupName, 
        List<string> exceptReceiveConnectionIds,
        string message, 
        bool isClientInvoke = false)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var exceptReceiveConnectionId in exceptReceiveConnectionIds)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = exceptReceiveConnectionId,
                ToGroup = groupName,
                PushContent = message,
                Remark = "ExceptUser",
                EventType = (isClientInvoke ? EnumEventType.SignalrClientInvoke : EnumEventType.SignalrSendMessage).GetDescription()
            });
        }
        return base.InsertRange(entities);
    }
}