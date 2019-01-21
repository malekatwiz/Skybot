using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public class IntentService : IIntentService
    {
        private readonly IIntentFactory _intentFactory;

        public IntentService(IIntentFactory intentFactory)
        {
            _intentFactory = intentFactory;
        }

        public Task<RecognitionResult> Execute(string intentName, IList<LuisEntity> entities)
        {
            return _intentFactory.CreateIntent(intentName).Execute(entities);
        }
    }
}
