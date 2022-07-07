using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using OdinModels.Core.Entities;
using OdinMSA.SnowFlake;
using SqlSugar;
using Type = Google.Protobuf.WellKnownTypes.Type;

namespace OdinMSA.OdinEF;

public class RepositoryBase<T> : SimpleClient<T> where T : class, new()
{
    private IServiceCollection Services { get; }
    private IServiceProvider ServiceProvider { get; }
    protected RepositoryBase(ISqlSugarClient db) : base(db)
    {
        Services = new ServiceCollection();
        Services.AddSingletonSnowFlake(1,1);
        ServiceProvider = Services.BuildServiceProvider();
    }
    public override bool Insert(T insertObj)
    {
        var propertyInfo = insertObj.GetType().GetProperty("Id");
        System.Type idType = propertyInfo?.PropertyType;
        if (idType != null && String.Compare(idType.Name,"Int64",StringComparison.OrdinalIgnoreCase)==0)
        {
            if (Convert.ToInt32(propertyInfo.GetValue(insertObj)) == 0)
            {
                var snowFlake = ServiceProvider.GetService<IOdinSnowFlake>();
                if (snowFlake != null) 
                    propertyInfo.SetValue(insertObj, snowFlake.CreateSnowFlakeId());
            }
        }
        
        // if (snowFlake != null)
        // {
        //     var id = snowFlake.CreateSnowFlakeId();
        //     if (insertObj != null && insertObj.Id == 0) insertObj.Id = id;
        // }
        return base.Context.Insertable<T>(insertObj).ExecuteCommand() > 0;
    }

    /// <summary>
    /// 获取总数
    /// </summary>
    /// <returns></returns>
    protected virtual int GetCount()
    {
        return base.Context.Queryable<T>().ToList().Count;
    }
}