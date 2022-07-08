namespace OdinPush.SignalrPush.SignalRServices;

public interface ISignalREventMonitorService
{
    bool Connected(string connectionId,string message);
    bool Disconnected(string connectionId,string message);
    bool SendMessageToUser(string sendConnectionId,string receiveConnectionId, string message, bool isClientInvoke);
    bool SendMessageToUsers(string sendConnectionId,List<string> receiveConnectionIds, string message, bool isClientInvoke);
    bool SendMessageToExceptUsers(string sendConnectionId,List<string> exceptReceiveConnectionIds, string message, bool isClientInvoke);
    
    bool SendMessageToGroup(string sendConnectionId,string groupName, string message, bool isClientInvoke);
    bool SendMessageToGroups(string sendConnectionId,List<string> groups, string message, bool isClientInvoke);
    bool SendMessageToGroupsExceptUsers(
        string sendConnectionId,
        string groupName,List<string> exceptReceiveConnectionIds, 
        string message,
        bool isClientInvoke);
}