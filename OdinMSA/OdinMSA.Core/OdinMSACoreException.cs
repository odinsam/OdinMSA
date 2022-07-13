using System.ComponentModel;
using OdinModels.OdinUtils;
using OdinModels.OdinUtils.OdinExceptionExtensions;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.Core.Enums;

namespace OdinMSA.Core;
public class OdinMSACoreException : EnumOdinException
{
    [Description("addJsonFiles can not be null")] public static EnumException BuilderAddJsonFilex01;

    [Description("httpClientInfo can not be null")] public static EnumException BuilderAddHttpClientx01;
    
    [Description("WebApi Http Response Code:{0}")] public static EnumException WebApiHttpResponseCode;
    
    [Description("Consul Service not find")] public static EnumException RestTemplateServiceNotFind;
    
    
}