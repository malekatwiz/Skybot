using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Skybot.Api.IntegTests.Controllers
{
    [TestClass]
    public class StatusControllerTests : IntegTestBase
    {
        [TestMethod]
        public async Task Status_ReturnsOkStatusCode()
        {
            HttpClient.DefaultRequestHeaders.Clear();
            var result = await HttpClient.GetAsync($"{Config["SkybotApiUri"]}/api/status");

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
