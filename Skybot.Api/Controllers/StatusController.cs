using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Skybot.Api.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            return Ok("I'm doing okay");
        }
    }
}