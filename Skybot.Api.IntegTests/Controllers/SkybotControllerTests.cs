using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybot.Api.Models;

namespace Skybot.Api.IntegTests.Controllers
{
    [TestClass]
    public class SkybotControllerTests : IntegTestBase
    {
        [TestMethod]
        public async Task Process_ReturnsUnAuthorized_WhenBearerTokenNotProvided()
        {
            var result = await HttpClient.PostAsJsonAsync($"{Config["SkybotApiUri"]}/api/skybot/process",
                new QueryModel
                {
                    Query = "Hello"
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Unauthorized);
        }

        [TestMethod]
        public async Task Process_ReturnsOkStatusCode_WhenValidBearerTokenIsProvided()
        {
            var token = await GetTokenAsync();
            AddBearerToken(token);

            var result = await HttpClient.PostAsJsonAsync($"{Config["SkybotApiUri"]}/api/skybot/process",
                new QueryModel
                {
                    Query = "Hello!"
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Process_AcceptsQueryModelParameter()
        {
            var token = await GetTokenAsync();
            AddBearerToken(token);

            var result = await HttpClient.PostAsJsonAsync($"{Config["SkybotApiUri"]}/api/skybot/process",
                new QueryModel
                {
                    Query = "Hello!"
                });

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public async Task Process_RespondsWithResponseMessage_WhenGivenQueryWithToken()
        {
            var token = await GetTokenAsync();
            AddBearerToken(token);

            var result = await HttpClient.PostAsJsonAsync($"{Config["SkybotApiUri"]}/api/skybot/process",
                new QueryModel
                {
                    Query = "Did you feed the dog?"
                });

            var responseMessage = await result.Content.ReadAsStringAsync();

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(responseMessage));
        }
    }
}
