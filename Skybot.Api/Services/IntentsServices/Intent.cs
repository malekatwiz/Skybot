using Skybot.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skybot.Api.Services.IntentsServices
{
    public abstract class Intent
    {
        public abstract Task<RecognitionResult> Execute(IList<LuisEntity> entities);
    }
}
