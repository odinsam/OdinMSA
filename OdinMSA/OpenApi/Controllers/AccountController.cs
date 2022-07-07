using Microsoft.AspNetCore.Mvc;
using OdinMSA.OdinLog.Core;
using OdinMSA.OdinLog.Core.Models;

namespace OpenApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOdinLogs odinLog;
        public AccountController(IOdinLogs odinLog)
        {
            this.odinLog = odinLog;
        }

        [HttpGet]
        [Route("/account/login")]
        public bool UserLogin()
        {
            this.odinLog.Info(new LogInfo() { LogContent = "this is a log" });
            return true;
        }
    }
}