using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.IntegTests.Controllers;
using Skybot.Api.Services.Luis;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.IntegTests.Services.Luis
{
    [TestClass]
    public class LuisServiceTests : IntegTestBase
    {
        [TestMethod]
        public async Task Query_ReturnsModelWithIntents_WhenCommandQuered()
        {
            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.LuisAppUri)
                .Returns($"{Config["LuisApp:Uri"]}?subscription-key={Config["LuisApp:Key"]}&verbose=true&timezoneOffset=0");

            var luisService = new LuisService(settingsMock.Object);

            var result = await luisService.Query("Who let the dog out??");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Intents);
            Assert.AreNotEqual(result.Intents.Count, 0);
        }
    }
}
