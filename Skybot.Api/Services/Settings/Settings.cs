using Microsoft.Extensions.Configuration;

namespace Skybot.Api.Services.Settings
{
    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string LuisAppEndpoint => _configuration["LuisApp:Endpoint"];
        public string LuisAppKey => _configuration["LuisApp:Key"];
        public string LuisAppUri => $"{LuisAppEndpoint}?subscription-key={LuisAppKey}&verbose=true&timezoneOffset=0";
        public string TranslateApiKey => _configuration["TranslateApiCredentials:ApiKey"];
        public double IntentThreshold => double.Parse(_configuration["IntentThreshold"]);
    }
}
