using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public class HomeAutomationIntent : IHomeAutomationIntent
    {
        public Task<RecognitionResult> Execute(IList<LuisEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}
