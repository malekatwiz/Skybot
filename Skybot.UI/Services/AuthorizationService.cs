using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Skybot.UI.Models;
using Skybot.UI.Settings;

namespace Skybot.UI.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly ISettings _settings;

        public AuthorizationService(ISettings settings)
        {
            _settings = settings;
        }

        public async Task<string> GetTokenAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"client_id", _settings.SkybotAuthClientId },
                    {"client_secret", _settings.SkybotAuthClientSecret },
                    {"grant_type", "client_credentials" }
                });

                var response = await httpClient.PostAsync($"{_settings.SkybotAuthUri}/connect/token", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var deserializedContent = JsonConvert.DeserializeObject<dynamic>(responseContent);
                return deserializedContent.access_token;
            }
        }

        public async Task UserSignInAsync(HttpContext httpContext, UserAccountModel userAccount)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.Name),
                new Claim(ClaimTypes.MobilePhone, userAccount.PhoneNumber)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
