using Microsoft.Extensions.DependencyInjection;
using Skybot.Api.Models;
using Skybot.Api.Services.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Skybot.Api.Services.IntentsServices
{
    public class IntentResolver : IIntentResolver
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISettings _settings;

        public IntentResolver(IServiceProvider serviceProvider, ISettings settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings;
        }

        public Task<RecognitionResult> Resolve(LuisResultModel resultModel)
        {
            var intent = resultModel.Intents.OrderByDescending(x => x.Score).FirstOrDefault();
            var intentServices = _serviceProvider.GetServices<IIntentService>();

            if (CheckIntentScore(intent))
            {
                switch (intent.Name.ToLower())
                {
                    case "translate":
                        return intentServices.FirstOrDefault(x => typeof(TranslateIntent) == x.GetType()).Execute(resultModel);
                }
            }

            return intentServices.FirstOrDefault(x => typeof(NoneIntent) == x.GetType()).Execute(resultModel);
        }

        private bool CheckIntentScore(LuisIntent intent)
        {
            if (intent?.Score > _settings.IntentThreshold)
            {
                return true;
            }
            return false;
        }
    }
}
