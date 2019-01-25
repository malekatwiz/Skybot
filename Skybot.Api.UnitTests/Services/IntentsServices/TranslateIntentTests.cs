using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.Models;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.UnitTests.Services.IntentsServices
{
    [TestClass]
    public class TranslateIntentTests
    {
        private const string TranslateIntentErrorMessage = "Sorry, I don't understand what you asked me";

        [TestMethod]
        public async Task Execute_ReturnsRecognitionResultWithErrorMessage_WhenTextEntityIsMissing()
        {
            var testTranslateEntities = new List<LuisEntity>
            {
                new LuisEntity{Name = "Hello", Type = "Type1", Score = 0},
                new LuisEntity{Name = "French", Type = "Dictionary.TargetLanguage", Score = 0}               
            };

            var settingsMock = CreateSettingsMock();

            var translateIntent = new TranslateIntent(settingsMock.Object);

            var result = await translateIntent.Execute(testTranslateEntities);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, TranslateIntentErrorMessage);
        }

        [TestMethod]
        public async Task Execute_ReturnsRecognitionResultWithErrorMessage_WhenTextEntityIsEmpty()
        {
            var testTranslateEntities = new List<LuisEntity>
            {
                new LuisEntity{Name = string.Empty, Type = "Dictionary.Text", Score = 0},
                new LuisEntity{Name = "French", Type = "Dictionary.TargetLanguage", Score = 0}
            };

            var settingsMock = CreateSettingsMock();

            var translateIntent = new TranslateIntent(settingsMock.Object);

            var result = await translateIntent.Execute(testTranslateEntities);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, TranslateIntentErrorMessage);
        }

        [TestMethod]
        public async Task Execute_ReturnsRecognitionResultWithErrorMessage_WhenTargetLanguageEntityIsMissing()
        {
            var testTranslateEntities = new List<LuisEntity>
            {
                new LuisEntity{Name = "Hello", Type = "Dictionary.Text", Score = 0},
                new LuisEntity{Name = "French", Type = "Type2", Score = 0}
            };

            var settingsMock = CreateSettingsMock();            

            var translateIntent = new TranslateIntent(settingsMock.Object);

            var result = await translateIntent.Execute(testTranslateEntities);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, TranslateIntentErrorMessage);
        }

        [TestMethod]
        public async Task Execute_ReturnsRecognitionResultWithErrorMessage_WhenTargetLanguageIsEmpty()
        {
            var testTranslateEntities = new List<LuisEntity>
            {
                new LuisEntity{Name = "Hello", Type = "Dictionary.Text", Score = 0},
                new LuisEntity{Name = string.Empty, Type = "Dictionary.TargetLanguage", Score = 0}
            };

            var settingsMock = CreateSettingsMock();

            var translateIntent = new TranslateIntent(settingsMock.Object);

            var result = await translateIntent.Execute(testTranslateEntities);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, TranslateIntentErrorMessage);
        }

        private Mock<ISettings> CreateSettingsMock()
        {
            var settingsMock = new Mock<ISettings>();
            settingsMock.Setup(x => x.TranslateApiKey).Returns("Key");

            return settingsMock;
        }
    }
}
