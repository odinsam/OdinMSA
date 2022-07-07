using Microsoft.AspNetCore.Mvc;
using OdinMSA.OpenApi.Services;

namespace OdinMSA.OpenApi.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        // private readonly IOdinLogs _odinLog;
        public AccountController(IPushRecordSignalRServices pushRecordSignalRServices)
        {
            // this._odinLog = odinLog;
        }

        [HttpGet]
        [Route("/account/login")]
        public bool UserLogin(string userName)
        {
            // this._odinLog.Info(new LogInfo() { LogContent = $"{userName} is login" });
            return true;
        }
        
        [HttpGet]
        [Route("/account/count")]
        public int GetCount()
        {
            // return _pushRecordSignalRServices.GetUserCount();
            return 0;
        }
    }
}