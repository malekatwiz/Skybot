using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
using Skybot.Api.Services.Settings;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services.IntentsServices
{
    public class TranslateIntent : IIntentService
    {
        private readonly ISettings settings;

        public TranslateIntent(ISettings settings)
        {
            this.settings = settings;
        }

        public async Task<RecognitionResult> Execute(LuisResultModel model)
        {
            var client = CreateClient();

            var translation = await client.TranslateTextAsync(GetTargetText(model), Languages(client)[GetTargetLanguage(model)]);

            return new RecognitionResult
            {
                Message = translation.TranslatedText
            };
        }

        private IReadOnlyDictionary<string, string> Languages(TranslationClient client)
        {
            return client.ListLanguages("en").ToDictionary(x => x.Name, x => x.Code, StringComparer.InvariantCultureIgnoreCase);
        }

        private string GetTargetText(LuisResultModel model)
        {
            return model.Entities.FirstOrDefault(x => x.Type.Equals("Dictionary.Text"))?.Name;
        }

        private string GetTargetLanguage(LuisResultModel model)
        {
            return model.Entities.FirstOrDefault(x => x.Type.Equals("Dictionary.TargetLanguage"))?.Name;
        }

        private TranslationClient CreateClient()
        {
            return TranslationClient.CreateFromApiKey(settings.TranslateApiKey);
        }
    }
}
