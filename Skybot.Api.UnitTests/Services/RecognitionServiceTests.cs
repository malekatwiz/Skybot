using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.Models;
using Skybot.Api.Services;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Luis;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.UnitTests.Services
{
    [TestClass]
    public class RecognitionServiceTests
    {
        [TestMethod]
        public async Task Process_ReturnsNonEmptyRecognitionResult_WhenIntentHasRequiredScore()
        {
            var query = "I'm testing you";
            var luisResultModel = CreateTestLuisResultModel();
            var recognitionResult = new RecognitionResult { Message = "I will pass your test" };

            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.IntentThreshold).Returns(0.75);

            var intentServiceMock = new Mock<IIntentService>();
            var intentWithHighestScore = luisResultModel.Intents.OrderByDescending(x => x.Score).FirstOrDefault();
            intentServiceMock.Setup(x => x.Execute(intentWithHighestScore.Name, luisResultModel.Entities))
                .Returns(Task.FromResult(recognitionResult))
                .Verifiable();

            var luisServiceMock = CreateLuisServiceMock(luisResultModel, query);

            var loggerMock = new Mock<ILogger<RecognitionService>>();

            var recognitionService = new RecognitionService(settingsMock.Object,
                intentServiceMock.Object,
                luisServiceMock.Object,
                loggerMock.Object);

            var result = await recognitionService.Process(query);

            luisServiceMock.Verify();
            intentServiceMock.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, recognitionResult);
        }

        [TestMethod]
        public async Task Process_ReturnsEmptyRecognitionResult_WhenIntentScoreIsLessThanRequired()
        {
            var query = "I'm testing you";
            var luisResultModel = CreateTestLuisResultModel();
            var recognitionResult = new RecognitionResult { Message = string.Empty };

            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.IntentThreshold).Returns(0.9);

            var intentServiceMock = new Mock<IIntentService>();
            intentServiceMock.Setup(x => x.Execute(string.Empty, null))
                .Returns(Task.FromResult(recognitionResult))
                .Verifiable();

            var luisServiceMock = CreateLuisServiceMock(luisResultModel, query);

            var loggerMock = new Mock<ILogger<RecognitionService>>();

            var recognitionService = new RecognitionService(settingsMock.Object,
                intentServiceMock.Object,
                luisServiceMock.Object,
                loggerMock.Object);

            var result = await recognitionService.Process(query);

            luisServiceMock.Verify();
            intentServiceMock.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, recognitionResult);
        }

        private LuisResultModel CreateTestLuisResultModel()
        {
            return new LuisResultModel
            {
                Entities = new List<LuisEntity>
                {
                    new LuisEntity{ Name = "Entity1", Type = "EntityType1", Score = 0.5},
                    new LuisEntity{ Name = "Entity2", Type = "EntityType2", Score = 0.2},
                    new LuisEntity{ Name = "Entity3", Type = "EntityType3", Score = 0.8}
                },
                Intents = new List<LuisIntent>
                {
                    new LuisIntent{Name = "Intent1", Score = 0.2},
                    new LuisIntent{Name = "Intent2", Score = 0.5},
                    new LuisIntent{Name = "Intent3", Score = 0.8}
                }
            };
        }

        private Mock<ILuisService> CreateLuisServiceMock(LuisResultModel model, string query)
        {
            var luisServiceMock = new Mock<ILuisService>();
            luisServiceMock.Setup(x => x.Query(query))
                .Returns(Task.FromResult(model))
                .Verifiable();

            return luisServiceMock;
        }
    }
}
