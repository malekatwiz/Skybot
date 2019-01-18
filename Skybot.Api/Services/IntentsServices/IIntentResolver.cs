using Skybot.Api.Models;
using System.Threading.Tasks;

namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntentResolver
    {
        Task<RecognitionResult> Resolve(LuisResultModel resultModel);
    }
}
