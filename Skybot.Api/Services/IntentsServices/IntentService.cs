using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public class IntentService : IIntentService
    {
        private readonly IDictionary<string, IIntent> _intents;

        public IntentService(ITranslateIntent translateIntent, INonIntent nonIntent)
        {
            _intents = new Dictionary<string, IIntent>
            {
                {IntentType.Translate, translateIntent},
                {IntentType.NonIntent, nonIntent}
            };
        }

        public Task<RecognitionResult> Execute(string intentName, IList<LuisEntity> entities)
        {
            return CreateIntent(intentName.ToLower()).Execute(entities);
        }

        private IIntent CreateIntent(string intentName)
        {
            return _intents.ContainsKey(intentName) ? _intents[intentName] : _intents[IntentType.NonIntent];
        }
    }
}
