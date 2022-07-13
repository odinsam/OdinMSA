using ServiceStack.Redis;

namespace OdinMSA.OdinRedis;

public class RedisListService:RedisBase
{
    #region 赋值
    /// <summary>
    /// 从左侧向list中添加值
    /// </summary>
    public void LPush(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PushItemToList(key, value);
    }
    /// <summary>
    /// 从左侧向list中添加值，并设置过期时间
    /// </summary>
    public void LPush(string key, string value, DateTime dt,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PushItemToList(key, value);
        base.IClient.ExpireEntryAt(key, dt);
    }
    /// <summary>
    /// 从左侧向list中添加值，设置过期时间
    /// </summary>
    public void LPush(string key, string value, TimeSpan sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PushItemToList(key, value);
        base.IClient.ExpireEntryIn(key, sp);
    }
    /// <summary>
    /// 从右侧向list中添加值
    /// </summary>
    public void RPush(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PrependItemToList(key, value);
    }
    /// <summary>
    /// 从右侧向list中添加值，并设置过期时间
    /// </summary>   
    public void RPush(string key, string value, DateTime dt,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PrependItemToList(key, value);
        base.IClient.ExpireEntryAt(key, dt);
    }
    /// <summary>
    /// 从右侧向list中添加值，并设置过期时间
    /// </summary>       
    public void RPush(string key, string value, TimeSpan sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.PrependItemToList(key, value);
        base.IClient.ExpireEntryIn(key, sp);
    }
    /// <summary>
    /// 添加key/value
    /// </summary>    
    public void Add(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddItemToList(key, value);
    }
    /// <summary>
    /// 添加key/value ,并设置过期时间
    /// </summary> 
    public void Add(string key, string value, DateTime dt,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddItemToList(key, value);
        base.IClient.ExpireEntryAt(key, dt);
    }
    /// <summary>
    /// 添加key/value。并添加过期时间
    /// </summary> 
    public void Add(string key, string value, TimeSpan sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddItemToList(key, value);
        base.IClient.ExpireEntryIn(key, sp);
    }
    /// <summary>
    /// 为key添加多个值
    /// </summary> 
    public void Add(string key, List<string> values,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddRangeToList(key, values);
    }
    /// <summary>
    /// 为key添加多个值，并设置过期时间
    /// </summary> 
    public void Add(string key, List<string> values, DateTime dt,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddRangeToList(key, values);
        base.IClient.ExpireEntryAt(key, dt);
    }
    /// <summary>
    /// 为key添加多个值，并设置过期时间
    /// </summary> 
    public void Add(string key, List<string> values, TimeSpan sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddRangeToList(key, values);
        base.IClient.ExpireEntryIn(key, sp);
    }
    #endregion

    #region 获取值
    /// <summary>
    /// 获取list中key包含的数据数量
    /// </summary> 
    public long Count(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetListCount(key);
    }
    /// <summary>
    /// 获取key包含的所有数据集合
    /// </summary> 
    public List<string> Get(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetAllItemsFromList(key);
    }
    /// <summary>
    /// 获取key中下标为star到end的值集合
    /// </summary> 
    public List<string> Get(string key, int star, int end,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetRangeFromList(key, star, end);
    }
    #endregion

    #region 阻塞命令
    /// <summary>
    ///  阻塞命令：从list为key的尾部移除一个值，并返回移除的值，阻塞时间为sp
    /// </summary> 
    public string BlockingPopItemFromList(string key, TimeSpan? sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.BlockingPopItemFromList(key, sp);
    }
    /// <summary>
    ///  阻塞命令：从多个list中尾部移除一个值,并返回移除的值&key，阻塞时间为sp
    /// </summary> 
    public ItemRef BlockingPopItemFromLists(string[] keys, TimeSpan? sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.BlockingPopItemFromLists(keys, sp);
    }


    /// <summary>
    ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
    /// </summary> 
    public string BlockingDequeueItemFromList(string key, TimeSpan? sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.BlockingDequeueItemFromList(key, sp);
    }

    /// <summary>
    /// 阻塞命令：从多个list中尾部移除一个值，并返回移除的值&key，阻塞时间为sp
    /// </summary> 
    public ItemRef BlockingDequeueItemFromLists(string[] keys, TimeSpan? sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.BlockingDequeueItemFromLists(keys, sp);
    }

    /// <summary>
    /// 阻塞命令：从list中一个fromkey的尾部移除一个值，添加到另外一个tokey的头部，并返回移除的值，阻塞时间为sp
    /// </summary> 
    public string BlockingPopAndPushItemBetweenLists(string fromkey, string tokey, TimeSpan? sp,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.BlockingPopAndPushItemBetweenLists(fromkey, tokey, sp);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 从尾部移除数据，返回移除的数据
    /// </summary> 
    public string PopItemFromList(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        var sa = base.IClient.CreateSubscription();
        return base.IClient.PopItemFromList(key);
    }
    /// <summary>
    /// 从尾部移除数据，返回移除的数据
    /// </summary> 
    public string DequeueItemFromList(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.DequeueItemFromList(key);
    }

    /// <summary>
    /// 移除list中，key/value,与参数相同的值，并返回移除的数量
    /// </summary> 
    public long RemoveItemFromList(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.RemoveItemFromList(key, value);
    }
    /// <summary>
    /// 从list的尾部移除一个数据，返回移除的数据
    /// </summary> 
    public string RemoveEndFromList(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.RemoveEndFromList(key);
    }
    /// <summary>
    /// 从list的头部移除一个数据，返回移除的值
    /// </summary> 
    public string RemoveStartFromList(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.RemoveStartFromList(key);
    }
    #endregion

    #region 其它
    /// <summary>
    /// 从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
    /// </summary> 
    public string PopAndPushItemBetweenLists(string fromKey, string toKey,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.PopAndPushItemBetweenLists(fromKey, toKey);
    }


    public void TrimList(string key, int start, int end,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.TrimList(key, start, end);
    }
    #endregion

    
}