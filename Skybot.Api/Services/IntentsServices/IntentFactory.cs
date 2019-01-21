using Skybot.Api.Services.Settings;

namespace Skybot.Api.Services.IntentsServices
{
    public class IntentFactory : IIntentFactory
    {
        private readonly ISettings _settings;

        public IntentFactory(ISettings settings)
        {
            _settings = settings;
        }

        public Intent CreateIntent(string intentName)
        {
            switch(intentName.ToLower())
            {
                case "translate":
                    return new TranslateIntent(_settings);
                default:
                    return new NoneIntent();
            }
        }
    }
}
