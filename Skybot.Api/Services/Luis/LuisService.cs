using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Skybot.Api.Models;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services.Luis
{
    public class LuisService : ILuisService
    {
        private readonly ISettings _settings;

        public LuisService(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<LuisResultModel> Query(string message)
        {
            var serializedResult = await CallSkybotApp(message);

            return !string.IsNullOrEmpty(serializedResult) ? JsonConvert.DeserializeObject<LuisResultModel>(serializedResult) : null;
        }

        private async Task<string> CallSkybotApp(string query)
        {
            var httpClient = new HttpClient();

            var encodedQuery = HttpUtility.UrlEncode(query);

            var response = await httpClient.GetAsync($"{_settings.LuisAppUri}&q={encodedQuery}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
