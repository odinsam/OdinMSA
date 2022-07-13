﻿namespace OdinMSA.OdinRedis;

public class RedisSetService : RedisBase
{
    #region 添加
    /// <summary>
    /// key集合中添加value值
    /// </summary>
    public void Add(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddItemToSet(key, value);
    }
    /// <summary>
    /// key集合中添加list集合
    /// </summary>
    public void Add(string key, List<string> list,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.AddRangeToSet(key, list);
    }
    #endregion

    #region 获取
    /// <summary>
    /// 随机获取key集合中的一个值
    /// </summary>
    public string GetRandomItemFromSet(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetRandomItemFromSet(key);
    }
    /// <summary>
    /// 获取key集合值的数量
    /// </summary>
    public long GetCount(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetSetCount(key);
    }
    /// <summary>
    /// 获取所有key集合的值
    /// </summary>
    public HashSet<string> GetAllItemsFromSet(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetAllItemsFromSet(key);
    }
    #endregion

    #region 删除
    /// <summary>
    /// 随机删除key集合中的一个值
    /// </summary>
    public string RandomRemoveItemFromSet(string key,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        return base.IClient.PopItemFromSet(key);
    }
    /// <summary>
    /// 删除key集合中的value
    /// </summary>
    public void RemoveItemFromSet(string key, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.RemoveItemFromSet(key, value);
    }
    #endregion

    #region 其它
    /// <summary>
    /// 从fromkey集合中移除值为value的值，并把value添加到tokey集合中
    /// </summary>
    public void MoveBetweenSets(string fromkey, string tokey, string value,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.MoveBetweenSets(fromkey, tokey, value);
    }
    /// <summary>
    /// 返回keys多个集合中的并集，返还hashset
    /// </summary>
    public HashSet<string> GetUnionFromSets(int clientDb=0,params string[] keys)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetUnionFromSets(keys);
    }
    /// <summary>
    /// 返回keys多个集合中的交集，返还hashset
    /// </summary>
    public HashSet<string> GetIntersectFromSets(int clientDb=0,params string[] keys)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetIntersectFromSets(keys);
    }
    /// <summary>
    /// 返回keys多个集合中的差集，返还hashset
    /// </summary>
    /// <param name="fromKey">原集合</param>
    /// <param name="keys">其他集合</param>
    /// <returns>出现在原集合，但不包含在其他集合</returns>
    public HashSet<string> GetDifferencesFromSet(string fromKey,int clientDb=0, params string[] keys)
    {
        base.IClient.Db = clientDb;
        return base.IClient.GetDifferencesFromSet(fromKey, keys);
    }
    /// <summary>
    /// keys多个集合中的并集，放入newkey集合中
    /// </summary>
    public void StoreUnionFromSets(string newkey, string[] keys,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.StoreUnionFromSets(newkey, keys);
    }
    /// <summary>
    /// 把fromkey集合中的数据与keys集合中的数据对比，fromkey集合中不存在keys集合中，则把这些不存在的数据放入newkey集合中
    /// </summary>
    public void StoreDifferencesFromSet(string newkey, string fromkey, string[] keys,int clientDb=0)
    {
        base.IClient.Db = clientDb;
        base.IClient.StoreDifferencesFromSet(newkey, fromkey, keys);
    }
    #endregion
}