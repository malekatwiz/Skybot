using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Skybot.UI.Settings;

namespace Skybot.UI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettings _settings;

        public AuthorizationService(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetToken()
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _settings.SkybotAuthClientId },
                    {"client_secret", _settings.SkybotAuthClientSecret },
                    {"grant_type", "client_credentials" }
                });

                var response = await httpClient.PostAsync($"{_settings.SkybotAuthUri}/connect/token", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var deserializedContent = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return deserializedContent.access_token;
            }
        }
    }
}
