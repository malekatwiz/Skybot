using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Skybot.Api.IntegTests.Controllers
{
    public class IntegTestBase
    {
        protected IConfiguration Config;
        protected HttpClient HttpClient;

        [TestInitialize]
        public void Init()
        {
            Config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            HttpClient = new HttpClient();
        }

        protected async Task<string> GetTokenAsync()
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"client_id", $"{Config["SkybotAuth:ClientId"]}"},
                {"client_secret", $"{Config["SkybotAuth:ClientSecret"]}" },
                {"grant_type", "client_credentials" }
            });

            HttpClient.DefaultRequestHeaders.Clear();
            var response = await HttpClient.PostAsync(Config["SkybotAuth:Uri"], content);
            var responseContent = await response.Content.ReadAsStringAsync();

            var deserializedResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return deserializedResponse.access_token;
        }

        protected void AddBearerToken(string token)
        {
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
    }
}