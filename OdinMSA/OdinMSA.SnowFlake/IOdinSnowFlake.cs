namespace OdinMSA.SnowFlake;

public interface IOdinSnowFlake
{
    /// <summary>
    /// 创建雪花Id
    /// </summary>
    /// <returns></returns>
    long CreateSnowFlakeId();
    
    /// <summary>
    /// 解析Id是否是雪花Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    string AnalyzeId(long id);
}