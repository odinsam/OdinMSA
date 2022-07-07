using Microsoft.Extensions.DependencyInjection;
using OdinModels.Core.Entities;
using OdinMSA.SnowFlake;
using SqlSugar;

namespace OdinMSA.OdinEF;
public class Repository<T> : RepositoryBase<T> where T : class, new()
{
    protected Repository(ISqlSugarClient context = null) : base(context)//注意这里要有默认值等于null
    {
        base.Context=context;
    }
}