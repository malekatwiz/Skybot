using Microsoft.Extensions.Configuration;

namespace Skybot.UI.Settings
{
    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;

        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string SkybotAuthUri => _configuration["SkybotAuth:Uri"];
        public string SkybotAuthClientId => _configuration["SkybotAuth:ClientId"];
        public string SkybotAuthClientSecret => _configuration["SkybotAuth:ClientSecret"];
        public string SkybotAccountsUri => _configuration["SkybotAccounts:Uri"];
        public string SkybotTextoUri => _configuration["SkybotTexto:Uri"];
        public string SkybotApiUri => _configuration["SkybotApi:Uri"];
    }
}
