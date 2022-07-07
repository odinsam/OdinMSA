using OdinModels.OdinSignalR.Entities;
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
    
    public bool Connected(string connectionId)
    {
        return true;
    }
    
    public bool Disconnected(string connectionId)
    {
        return true;
    }

    public bool SendMessageToUser(string sendConnectionId, string receiveConnectionId, string message)
    {
        return base.Insert(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToUser = receiveConnectionId,
                PushContent = message,
            });
    }

    public bool SendMessageToUsers(string sendConnectionId, List<string> receiveConnectionIds, string message)
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
        return base.InsertRange(entities);
    }

    public bool SendMessageToExceptUsers(string sendConnectionId, List<string> exceptReceiveConnectionIds, string message)
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
        return base.InsertRange(entities);
    }

    public bool SendMessageToGroup(string sendConnectionId, string groupName, string message)
    {
        return base.Insert(new PushRecordSignalREntity()
            {
                FromUser = sendConnectionId,
                ToGroup = groupName,
                PushContent = message,
            });
    }

    public bool SendMessageToGroups(string sendConnectionId, List<string> groups, string message)
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
        return base.InsertRange(entities);
    }

    public bool SendMessageToGroupsExceptUsers(string sendConnectionId, string groupName, List<string> exceptReceiveConnectionIds,
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
        return base.InsertRange(entities);
    }
}