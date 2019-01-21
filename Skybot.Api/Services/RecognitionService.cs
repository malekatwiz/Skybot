using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services
{
    public class RecognitionService : IRecognitionService
    {
        private readonly ISettings _settings;
        private readonly ILogger _logger;
        private readonly IIntentService _intentService;

        public RecognitionService(ISettings settings, IIntentService intentService, ILogger<RecognitionService> logger)
        {
            _settings = settings;
            _intentService = intentService;
            _logger = logger;
        }

        public async Task<RecognitionResult> Process(string message)
        {
            try
            {
                var recognitionIntents = await GetQueryIntents(message);
                var intent = recognitionIntents.Intents.OrderByDescending(x => x.Score).FirstOrDefault();

                if (CheckIntentScore(intent))
                {
                    return await _intentService.Execute(intent?.Name, recognitionIntents.Entities);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has been caught");
            }
            return await _intentService.Execute(string.Empty, null);
        }

        private async Task<string> CallSkybotApp(string query)
        {
            var httpClient = new HttpClient();

            var encodedQuery = HttpUtility.UrlEncode(query);

            var response = await httpClient.GetAsync($"{_settings.LuisAppUri}&q={encodedQuery}");
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<LuisResultModel> GetQueryIntents(string query)
        {
            var serializedResult = await CallSkybotApp(query);

            return !string.IsNullOrEmpty(serializedResult) ? JsonConvert.DeserializeObject<LuisResultModel>(serializedResult) : null;
        }

        private bool CheckIntentScore(LuisIntent intent)
        {
            if (intent?.Score > _settings.IntentThreshold)
            {
                return true;
            }
            return false;
        }
    }
}
