namespace OdinMSA.OdinEF;

public abstract class AbsRepositoryServices<T> : Repository<T> where T : class, new()
{
    protected AbsRepositoryServices(SqlSugar.ISqlSugarClient db) : base(db) { }
}