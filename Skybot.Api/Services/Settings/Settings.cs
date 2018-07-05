using Microsoft.Extensions.Configuration;

namespace Skybot.Api.Services.Settings
{
    public class Settings : ISettings
    {
        private readonly IConfiguration configuration;

        public Settings(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string LuisAppEndpoint => configuration["LuisApp:Endpoint"];
        public string LuisAppKey => configuration["LuisApp:Key"];
        public string LuisAppUri => $"{LuisAppEndpoint}?subscription-key={LuisAppKey}&verbose=true&timezoneOffset=0";
        public string TranslateApiKey => configuration["TranslateApiCredentials:ApiKey"];
        public string Auth0TokenUri => $"https://{configuration["Auth0:Domain"]}/oauth/token";
        public double IntentThreshold => double.Parse(configuration["IntentThreshold"]);
    }
}
