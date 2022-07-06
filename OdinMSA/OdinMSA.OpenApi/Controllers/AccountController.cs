using Microsoft.AspNetCore.Mvc;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;
using OdinMSA.OpenApi.Services;

namespace OdinMSA.OpenApi.Controllers
{
    [Controller]
    public class AccountController : ControllerBase
    {
        private readonly IOdinLogs _odinLog;
        private readonly IPushRecordSignalRServices _pushRecordSignalRServices;
        public AccountController(IOdinLogs odinLog,IPushRecordSignalRServices pushRecordSignalRServices)
        {
            this._odinLog = odinLog;
            this._pushRecordSignalRServices = pushRecordSignalRServices;
        }

        [HttpGet]
        [Route("/account/login")]
        public bool UserLogin(string userName)
        {
            this._odinLog.Info(new LogInfo() { LogContent = $"{userName} is login" });
            return true;
        }
        
        [HttpGet]
        [Route("/account/count")]
        public int GetCount()
        {
            return _pushRecordSignalRServices.GetUserCount();
        }
    }
}