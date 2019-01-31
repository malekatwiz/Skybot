using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntent
    {
        Task<RecognitionResult> Execute(IList<LuisEntity> entities);
    }
}
