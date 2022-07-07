namespace OdinPush.SignalrPush.SignalRServices;

public interface ISignalREventMonitorService
{
    bool Connected(string connectionId);
    bool Disconnected(string connectionId);
    bool SendMessageToUser(string sendConnectionId,string receiveConnectionId, string message);
    bool SendMessageToUsers(string sendConnectionId,List<string> receiveConnectionIds, string message);
    bool SendMessageToExceptUsers(string sendConnectionId,List<string> exceptReceiveConnectionIds, string message);
    
    bool SendMessageToGroup(string sendConnectionId,string groupName, string message);
    bool SendMessageToGroups(string sendConnectionId,List<string> groups, string message);
    bool SendMessageToGroupsExceptUsers(string sendConnectionId,string groupName,List<string> exceptReceiveConnectionIds, string 
    message);
}