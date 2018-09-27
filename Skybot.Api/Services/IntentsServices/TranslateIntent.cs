using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
using Skybot.Api.Models;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services.IntentsServices
{
    public class TranslateIntent : IIntentService
    {
        private readonly ISettings _settings;

        public TranslateIntent(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<RecognitionResult> Execute(LuisResultModel model)
        {
            var client = CreateClient();

            var targetText = GetTargetText(model);
            var targetLanguage = GetTargetLanguage(model);

            if (!string.IsNullOrEmpty(targetText) && !string.IsNullOrEmpty(targetLanguage))
            {
                var translation = await client.TranslateTextAsync(targetText, Languages(client)[targetLanguage]);
                return new RecognitionResult
                {
                    Message = translation.TranslatedText
                };
            }

            return new RecognitionResult {Message = "Sorry, I don't understand what you asked me"};
        }

        private static IReadOnlyDictionary<string, string> Languages(TranslationClient client)
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
            return TranslationClient.CreateFromApiKey(_settings.TranslateApiKey);
        }
    }
}
