using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntentService
    {
        Task<RecognitionResult> Execute(string intentName, IList<LuisEntity> entities);
    }
}
