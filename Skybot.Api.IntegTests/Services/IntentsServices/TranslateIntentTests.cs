using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.IntegTests.Controllers;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.IntegTests.Services.IntentsServices
{
    [TestClass]
    public class TranslateIntentTests : IntegTestBase
    {
        [TestMethod]
        public async Task Execute_ReturnsTranslation_WhenEntitiesHaveTextAndTargetLanguage()
        {
            var testEntities = new List<LuisEntity>
            {
                new LuisEntity {Score = 1, Type = "Dictionary.Text", Name = "Hello"},
                new LuisEntity {Score = 1, Type = "Dictionary.TargetLanguage", Name = "French"}
            };

            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.TranslateApiKey)
                .Returns(Config["GoogleTarnslateApiKey"]);

            var translateIntent = new TranslateIntent(settingsMock.Object);

            var result = await translateIntent.Execute(testEntities);

            Assert.IsNotNull(result);
            Assert.IsFalse(string.IsNullOrEmpty(result.Message));
            Assert.AreEqual(result.Message, "Bonjour");
        }
    }
}
