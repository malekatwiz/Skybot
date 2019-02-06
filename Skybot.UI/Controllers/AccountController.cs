using Microsoft.AspNetCore.Mvc;

namespace Skybot.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}