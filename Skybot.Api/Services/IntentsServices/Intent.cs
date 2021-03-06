﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Skybot.Api.Models;

namespace Skybot.Api.Services.IntentsServices
{
    public abstract class Intent
    {
        public abstract Task<RecognitionResult> Execute(IList<LuisEntity> entities);
    }
}
