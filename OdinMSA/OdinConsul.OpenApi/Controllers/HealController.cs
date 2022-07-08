using Microsoft.AspNetCore.Mvc;

namespace OdinConsul.OpenApi.Controllers;
[Controller]
public class HealController : Controller
{
    [Route("/test/show")]
    public string Show()
    {
        return "show";
    }
}