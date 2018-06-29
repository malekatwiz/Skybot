using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRecognitionService recognitionService;
        private readonly ISettings settings;

        public SkybotController(ISettings settings, IRecognitionService recognitionService)
        {
            this.settings = settings;
            this.recognitionService = recognitionService;
        }

        [Route("process")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Process([FromBody]RecognitionQuery message)
        {
            var result = await recognitionService.Process(message.Message);
            if (result != null)
            {
                return Ok(result.Message);
            }

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

            var response = await httpClient.PostAsJsonAsync(settings.Auth0TokenUri, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var tokenObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return tokenObject == null ? StatusCode((int)HttpStatusCode.Unauthorized) : (IActionResult)Ok(tokenObject.access_token);
        }
    }
}
