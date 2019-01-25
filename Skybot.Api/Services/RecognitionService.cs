using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Luis;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services
{
    public class RecognitionService : IRecognitionService
    {
        private readonly ISettings _settings;
        private readonly ILogger _logger;
        private readonly IIntentService _intentService;
        private readonly ILuisService _luisService;

        public RecognitionService(ISettings settings, IIntentService intentService, ILuisService luisService, ILogger<RecognitionService> logger)
        {
            _settings = settings;
            _intentService = intentService;
            _luisService = luisService;
            _logger = logger;
        }

        public async Task<RecognitionResult> Process(string message)
        {
            try
            {
                var recognitionIntents = await _luisService.Query(message);
                var intent = recognitionIntents.Intents.OrderByDescending(x => x.Score).FirstOrDefault();

                if (CheckIntentScore(intent))
                {
                    return await _intentService.Execute(intent?.Name, recognitionIntents.Entities);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception has been caught");
            }
            return await _intentService.Execute(string.Empty, null);
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
