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

            var translateIntentMock = new Mock<ITranslateIntent>();
            translateIntentMock.Setup(x => x.Execute(testEntities))
                .Returns(Task.FromResult(new RecognitionResult
                {
                    Message = "translated text"
                }));

            var nonIntentMock = new Mock<INonIntent>();

            var intentService = new IntentService(translateIntentMock.Object, nonIntentMock.Object);

            var result = await intentService.Execute("Translate", testEntities);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.Message, string.Empty);
        }
    }
}
