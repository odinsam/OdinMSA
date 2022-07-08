namespace OdinPush.SignalrPush
{
    public interface IOdinSignalRClient
    {
        Task Connected(string message);
        Task DisConnected(string message);
        Task SendMessage(string message);
    }
}
