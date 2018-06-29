using System;
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
            var client = TranslationClient.CreateFromApiKey(settings.TranslateApiKey);
            var targetText = model.Entities.FirstOrDefault(x => x.Type.Equals("Dictionary.Text"))?.Name;
            var targetLanguage = model.Entities.FirstOrDefault(x => x.Type.Equals("Dictionary.TargetLanguage"))?.Name;

            var languages = client.ListLanguages("en");
            var targetLanguageCode = languages.FirstOrDefault(x => string.Equals(x.Name, targetLanguage, StringComparison.InvariantCultureIgnoreCase));
            var translation = await client.TranslateTextAsync(targetText, targetLanguageCode?.Code);

            return new RecognitionResult
            {
                Message = translation.TranslatedText
            };
        }
    }
}
