using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntentService
    {
        Task<RecognitionResult> Execute(LuisResultModel model);
    }
}
