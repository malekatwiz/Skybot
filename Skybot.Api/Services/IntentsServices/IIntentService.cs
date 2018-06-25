using System.Threading.Tasks;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntentService
    {
        Task<RecognitionResult> Execute(LuisResultModel model);
    }
}
