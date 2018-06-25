﻿using System.Threading.Tasks;
using Skybot.Models.Skybot;

namespace Skybot.Api.Services.IntentsServices
{
    public class NoneIntent : IIntentService
    {
        public Task<RecognitionResult> Execute(LuisResultModel model)
        {
            return Task.Run(() => new RecognitionResult {Message = "Sorry, I don't understand what you asked me"});
        }
    }
}
