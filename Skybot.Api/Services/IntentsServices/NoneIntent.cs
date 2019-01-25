using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public class NoneIntent : Intent
    {
        public NoneIntent()
        {

        }

        public override Task<RecognitionResult> Execute(IList<LuisEntity> entities)
        {
            return Task.Run(() => new RecognitionResult {Message = "Sorry, I don't understand what you asked me"});
        }
    }
}
