using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skybot.Api.Models;
using Skybot.Api.Services;

namespace Skybot.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SkybotController : ControllerBase
    {
        private readonly IRecognitionService _recognitionService;
        private readonly ILogger _logger;

        public SkybotController(IRecognitionService recognitionService, ILogger<SkybotController> logger)
        {
            _recognitionService = recognitionService;
            _logger = logger;
        }

        [Route("process")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Process([FromBody]RecognitionQuery message)
        {
            _logger.LogInformation($"Received new request to process: {message.Message}");

            var result = await _recognitionService.Process(message.Message);
            if (result != null)
            {
                return Ok(result.Message);
            }

            _logger.LogInformation("Failed to process incoming request");
            return StatusCode((int) HttpStatusCode.InternalServerError);
        }
    }
}
