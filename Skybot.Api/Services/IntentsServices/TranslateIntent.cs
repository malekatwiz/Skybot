using System;
using System.Threading.Tasks;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services.IntentsServices
{
    public class TranslateIntent : IIntentService
    {
        public Task<RecognitionResult> Execute(LuisResultModel model)
        {
            throw new NotImplementedException();
        }
    }
}
