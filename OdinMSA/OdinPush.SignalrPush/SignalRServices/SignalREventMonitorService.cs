using OdinModels.OdinSignalR.Entities;
using OdinMSA.OdinEF;
using OdinMSA.SnowFlake;
using SqlSugar;

namespace OdinPush.SignalrPush.SignalRServices;

public class SignalREventMonitorService : AbsRepositoryServices<PushRecordSignalREntity>,ISignalREventMonitorService
{
    private readonly IOdinSnowFlake _odinSnowFlake;
    public SignalREventMonitorService(ISqlSugarClient db,IOdinSnowFlake odinSnowFlake) : base(db)
    {
        this._odinSnowFlake = odinSnowFlake;
    }
    
    public void Connected(string connectionId)
    {
        
    }
    
    public void Disconnected(string connectionId)
    {
        
    }

    public void SendMessageToUser(string sendConnectionId, string receiveConnectionId, string message)
    {
        base.Insert(new PushRecordSignalREntity()
        {
            FromUser = sendConnectionId,
            ToUser = receiveConnectionId,
            PushContent = message,
        });
    }

    public void SendMessageToUsers(string sendConnectionId, List<string> receiveConnectionIds, string message)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var receiveConnectionId in receiveConnectionIds)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
            });
        }
        base.InsertRange(entities);
    }

    public void SendMessageToExceptUsers(string sendConnectionId, List<string> exceptReceiveConnectionIds, string message)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var receiveConnectionId in exceptReceiveConnectionIds)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
                Remark = "ExceptUser"
            });
        }
        base.InsertRange(entities);
    }

    public void SendMessageToGroup(string sendConnectionId, string groupName, string message)
    {
        base.Insert(new PushRecordSignalREntity()
        {
            FromUser = sendConnectionId,
            ToGroup = groupName,
            PushContent = message,
        });
    }

    public void SendMessageToGroups(string sendConnectionId, List<string> groups, string message)
    {
        var entities = new List<PushRecordSignalREntity>();
        foreach (var group in groups)  
        {
            entities.Add(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToGroup = group,
                PushContent = message,
            });
        }
        base.InsertRange(entities);
    }

    public void SendMessageToGroupsExceptUsers(string sendConnectionId, string groupName, List<string> exceptReceiveConnectionIds,
        string message)
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
                Remark = "ExceptUser"
            });
        }
        base.InsertRange(entities);
    }
}