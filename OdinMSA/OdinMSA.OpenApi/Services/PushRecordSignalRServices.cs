using OdinModels.OdinSignalR.Entities;
using OdinMSA.OdinEF;
using SqlSugar;

namespace OdinMSA.OpenApi.Services;
public class PushRecordSignalRServices : Repository<PushRecordSignalREntity>,IPushRecordSignalRServices
{
    public PushRecordSignalRServices(ISqlSugarClient db) : base(db)
    {
    }
    
    /// <summary>
    /// 获取总数
    /// </summary>
    /// <returns></returns>
    public int GetUserCount()
    {
        return base.GetCount();
    }

    
}