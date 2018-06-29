using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
using Microsoft.Extensions.Configuration;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services.IntentsServices
{
    public class TranslateIntent : IIntentService
    {
        private readonly IConfiguration configuration;

        public TranslateIntent(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<RecognitionResult> Execute(LuisResultModel model)
        {
            var client = TranslationClient.CreateFromApiKey(configuration["TranslateApiCredentials:ApiKey"]);
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
