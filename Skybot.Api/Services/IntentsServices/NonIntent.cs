using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public class NonIntent : INonIntent
    {
        public Task<RecognitionResult> Execute(IList<LuisEntity> entities)
        {
            return Task.Run(() => new RecognitionResult {Message = "Sorry, I don't understand what you asked me"});
        }

        public new Type GetType() => typeof(NonIntent);
    }
}
