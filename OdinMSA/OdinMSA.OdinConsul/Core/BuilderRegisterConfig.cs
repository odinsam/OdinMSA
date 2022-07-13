using Microsoft.Extensions.Configuration;
using OdinModels.OdinConsul;
using OdinModels.OdinUtils.OdinExtensions;
using OdinMSA.Core.Extensions;

namespace OdinMSA.OdinConsul.Core;

public class RegisterConfigHelper
{
    public static RegisterConfig BuilderRegisterConfig(IConfiguration configuration)=>
        new RegisterConfig()
        {
            ConsulConfig = new ConsulConfig()
            {
                Protocol = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_CONSUL_PROTOCOL),
                IP = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_CONSUL_IP),
                Port = configuration.GetSectionValue<int>(SystemConstant.KEY_OF_CONSUL_PORT)
            },
            ServiceConfig = new ServiceConfig()
            {
                ServiceName = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_NAME),
                RegisterId = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_REGISTERID),
                HealthUrl = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_HEALTHURL),
                DeregisterCriticalServiceAfter = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_DEREGISTERCRITICALSERVICEAFTER).ToInt(),
                HealInterval = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_HEALINTERVAL).ToInt(),
                TimeOut = configuration.GetSectionValue<string>(SystemConstant.KEY_OF_SERVICE_TIMEOUT).ToInt(),
                Tags = configuration.GetSectionModel<string[]>(SystemConstant.KEY_OF_SERVICE_TAGS),
            }
        };
    
}