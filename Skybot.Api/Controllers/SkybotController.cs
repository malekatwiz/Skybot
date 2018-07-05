using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skybot.Api.Services;
using Skybot.Api.Services.Settings;
using Skybot.Models.Skybot;

namespace Skybot.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SkybotController : ControllerBase
    {
        private readonly IRecognitionService _recognitionService;
        private readonly ISettings _settings;
        private readonly ILogger _logger;

        public SkybotController(ISettings settings, IRecognitionService recognitionService, ILogger<SkybotController> logger)
        {
            _settings = settings;
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

        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> RequestToken(string clientId, string clientSecret, string identifier)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var requestContent = new
            {
                grant_type = "client_credentials",
                client_id = clientId,
                client_secret = clientSecret,
                audience = identifier
            };

            var response = await httpClient.PostAsJsonAsync(_settings.Auth0TokenUri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var tokenObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return tokenObject == null ? StatusCode((int)HttpStatusCode.Unauthorized) : (IActionResult)Ok(tokenObject.access_token);
        }
    }
}
