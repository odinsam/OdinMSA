using Microsoft.AspNetCore.Mvc;
using OdinPush.SignalrPush.SignalRServices;

namespace OdinPush.SignalrPush.Controllers;

[Controller]
public class SignalRController:ControllerBase
{
    private  readonly ISignalREventMonitorService _eventMonitorService;
    public SignalRController(ISignalREventMonitorService eventMonitorService)
    {
        this._eventMonitorService = eventMonitorService;
    }
    [HttpPost]
    [Route("/signalr/sendmessage")]
    public void SendMessage(string connectionId,string message)
    {
        new OdinSignalR(_eventMonitorService).ServerSendMessageToUser(connectionId,message);
    }
}