using System.Collections.Generic;

namespace OdinMSA.Core.Models;

public class HttpClientInfo
{
    /// <summary>
    /// httpclient name
    /// </summary>
    public string ClientName { get; set; }
    
    /// <summary>
    /// httpclient base address
    /// </summary>
    public string BaseAddress { get; set; }
    
    /// <summary>
    /// httpclient base DefaultRequestHeaders
    /// </summary>
    public Dictionary<string,string> DefaultRequestHeaders { get; set; }

    /// <summary>
    /// Httpclient TimeOut
    /// </summary>
    public int TimeOut { get; set; }

    /// <summary>
    /// httpclient 所需要的证书
    /// </summary>
    public List<CertInfo> CertInfos { get; set; } = null;

}