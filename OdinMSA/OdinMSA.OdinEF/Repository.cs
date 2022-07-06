using SqlSugar;

namespace OdinMSA.OdinEF;

public class Repository<T> : SimpleClient<T> where T : class, new()
{
    protected Repository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
    {
        base.Context=context;
    }
 
    /// <summary>
    /// 获取总数
    /// </summary>
    /// <returns></returns>
    protected int GetCount()
    {
        return base.Context.Queryable<T>().ToList().Count;
    }
}