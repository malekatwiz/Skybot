using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skybot.UI.Models;
using Skybot.UI.Services;

namespace Skybot.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ISkybotService _skybotService;

        public HomeController(ISkybotService skybotService)
        {
            _skybotService = skybotService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SendQuery(string query)
        {
            var result = await _skybotService.SendQueryAsync(new SkybotQueryModel
            {
                Query = query
            });

            if (string.IsNullOrEmpty(result))
            {
                return new BadRequestResult();
            }

            return Ok(result);
        }
    }
}
