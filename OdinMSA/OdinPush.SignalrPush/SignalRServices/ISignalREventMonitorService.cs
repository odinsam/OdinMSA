namespace OdinPush.SignalrPush.SignalRServices;

public interface ISignalREventMonitorService
{
    void Connected(string connectionId);
    void Disconnected(string connectionId);
    void SendMessageToUser(string sendConnectionId,string receiveConnectionId, string message);
    void SendMessageToUsers(string sendConnectionId,List<string> receiveConnectionIds, string message);
    void SendMessageToExceptUsers(string sendConnectionId,List<string> exceptReceiveConnectionIds, string message);
    
    void SendMessageToGroup(string sendConnectionId,string groupName, string message);
    void SendMessageToGroups(string sendConnectionId,List<string> groups, string message);
    void SendMessageToGroupsExceptUsers(string sendConnectionId,string groupName,List<string> exceptReceiveConnectionIds, string 
    message);
}