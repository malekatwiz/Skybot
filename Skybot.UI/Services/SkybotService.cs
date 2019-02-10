using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skybot.UI.Models;
using Skybot.UI.Settings;

namespace Skybot.UI.Services
{
    public class SkybotService : ISkybotService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ISettings _settings;

        public SkybotService(IAuthorizationService authorizationService, ISettings settings)
        {
            _authorizationService = authorizationService;
            _settings = settings;
        }

        public async Task<string> SendQueryAsync(SkybotQueryModel model)
        {
            var accessToken = await _authorizationService.GetTokenAsync();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await httpClient.PostAsJsonAsync($"{_settings.SkybotApiUri}/api/skybot/process", model);

                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<string>(responseContent);
                }

                return string.Empty;
            }
        }
    }
}
