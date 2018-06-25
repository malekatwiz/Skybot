using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Skybot.Api.Services;
using Skybot.Models.Skybot;

namespace Skybot.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SkybotController : ControllerBase
    {
        private readonly IRecognitionService recognitionService;
        private readonly IConfiguration configuration;

        public SkybotController(IConfiguration configuration, IRecognitionService recognitionService)
        {
            this.configuration = configuration;
            this.recognitionService = recognitionService;
        }

        [Route("process")]
        [HttpPost]
        [Authorize]
        public IActionResult Process([FromBody]RecognitionQuery message)
        {
            return Ok(recognitionService.Process(message.Message));
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

            var response = await httpClient.PostAsJsonAsync($"https://{configuration["Auth0:Domain"]}/oauth/token", requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var tokenObject = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return tokenObject == null ? StatusCode((int)HttpStatusCode.Unauthorized) : (IActionResult)Ok(tokenObject.access_token);
        }
    }
}
