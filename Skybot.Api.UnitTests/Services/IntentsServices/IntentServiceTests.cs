using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Skybot.Api.UnitTests.Services.IntentsServices
{
    [TestClass]
    public class IntentServiceTests
    {
        [TestMethod]
        public async Task Execute_ReturnsNonEmptyRecognitionResult_WhenGivenValidIntent()
        {
            var testEntities = new List<LuisEntity>
            {
                new LuisEntity{Name = "", Type = "", Score = 0}
            };

            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.TranslateApiKey).Returns("key");

            var intentFactoryMock = new Mock<IIntentFactory>();
            intentFactoryMock.Setup(x => x.CreateIntent("Translate"))
                .Returns(new TranslateIntent(settingsMock.Object));

            var intentService = new IntentService(intentFactoryMock.Object);

            var result = await intentService.Execute("Translate", testEntities);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Message, string.Empty);
        }
    }
}
