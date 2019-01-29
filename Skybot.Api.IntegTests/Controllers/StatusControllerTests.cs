using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Skybot.Api.IntegTests.Controllers
{
    [TestClass]
    public class StatusControllerTests
    {
        [TestMethod]
        public async Task Status_ReturnsOkStatusCode()
        {
            var httpClient = new HttpClient();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var skybotApiUri = config["SkybotApiUri"];

            var result = await httpClient.GetAsync($"{skybotApiUri}/api/status");

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
