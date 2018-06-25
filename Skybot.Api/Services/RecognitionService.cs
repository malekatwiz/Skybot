using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Skybot.Api.Services.IntentsServices;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services
{
    public class RecognitionService : IRecognitionService
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider serviceProvider;

        public RecognitionService(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            this.configuration = configuration;
            this.serviceProvider = serviceProvider;
        }

        public async Task<RecognitionResult> Process(string message)
        {
            var recognitionIntents = await GetQueryIntents(message);

            return await ProcessIntents(recognitionIntents);
        }

        private Task<RecognitionResult> ProcessIntents(LuisResultModel model)
        {
            if (model != null)
            {
                var intent = model.Intents?.OrderByDescending(x => x.Score).FirstOrDefault();
                var intentRunnerType = typeof(NoneIntent);
                if (intent?.Score > 0.8)
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

            try
            {
                var response = await httpClient.GetAsync($"{configuration["LuisApp:Endpoint"]}?subscription-key={configuration["LuisApp:Key"]}&verbose=true&timezoneOffset=0&q={encodedQuery}");
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                // ignored
            }

            return string.Empty;
        }

        private async Task<LuisResultModel> GetQueryIntents(string query)
        {
            var serializedResult = await CallSkybotApp(query);

            return !string.IsNullOrEmpty(serializedResult) ? JsonConvert.DeserializeObject<LuisResultModel>(serializedResult) : null;
        }

        private IIntentService CreateIntentService(Type intentType)
        {
            var intentServices = serviceProvider.GetServices<IIntentService>();
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
