using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using ServiceStack.Redis;

namespace OdinMSA.OdinRedis;

public class RedisEventService : RedisBase
{
    private IOdinLogs _odinLogs;
    public RedisEventService(IOdinLogs odinLogs)
    {
        _odinLogs = odinLogs;
    }
    #region 发布订阅
    public void Publish(string channel, string message,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        _odinLogs.Info(new LogInfo(){ LogContent = $"频道{channel} 发布消息 {message}"});
        base.IClient.PublishMessage(channel, message);
    }
    
    /// <summary>
    /// 订阅消息
    /// </summary>
    /// <param name="channel">订阅的频道</param>
    /// <param name="actionOnMessage">订阅时间  string-频道  string-message  IRedisSubscription-订阅事件对象</param>
    /// <param name="clientDb"></param>
    public void Subscribe(string channel, Action<string, string, IRedisSubscription> actionOnMessage,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        var subscription = base.IClient.CreateSubscription();
        subscription.OnSubscribe = c =>
        {
            Console.WriteLine($"订阅频道{c}");
            Console.WriteLine();
        };
        //取消订阅
        subscription.OnUnSubscribe = c =>
        {
            Console.WriteLine($"取消订阅 {c}");
            Console.WriteLine();
        };
        subscription.OnMessage += (c, s) =>
        {
            actionOnMessage(c, s, subscription);
        };
        Console.WriteLine($"开始启动监听 {channel}");
        subscription.SubscribeToChannels(channel); //blocking
    }

    public void UnSubscribeFromChannels(string channel,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        var subscription = base.IClient.CreateSubscription();
        subscription.UnSubscribeFromChannels(channel);
    }
    #endregion
}