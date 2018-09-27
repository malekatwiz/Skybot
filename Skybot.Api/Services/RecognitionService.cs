using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services
{
    public class RecognitionService : IRecognitionService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISettings _settings;
        private readonly ILogger _logger;

        public RecognitionService(ISettings settings, IServiceProvider serviceProvider, ILogger<RecognitionService> logger)
        {
            _settings = settings;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task<RecognitionResult> Process(string message)
        {
            try
            {
                var recognitionIntents = await GetQueryIntents(message);

                return await ProcessIntents(recognitionIntents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has been caught");
            }
            return new RecognitionResult();
        }

        private Task<RecognitionResult> ProcessIntents(LuisResultModel model)
        {
            if (model != null)
            {
                var intent = model.Intents?.OrderByDescending(x => x.Score).FirstOrDefault();
                var intentRunnerType = typeof(NoneIntent);
                if (intent?.Score > _settings.IntentThreshold)
                {
                    intentRunnerType = IntentsDictionary[intent.Name];
                }

                return CreateIntentService(intentRunnerType).Execute(model);
            }

            return null;
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

        private IIntentService CreateIntentService(Type intentType)
        {
            var intentServices = _serviceProvider.GetServices<IIntentService>();
            return intentServices.FirstOrDefault(x => intentType == x.GetType());
        }

        private static IDictionary<string, Type> IntentsDictionary => new Dictionary<string, Type>
        {
            {"None", typeof(NoneIntent)},
            {"Who is", typeof(NoneIntent)},
            {"Translate", typeof(TranslateIntent)}
        };
    }
}
