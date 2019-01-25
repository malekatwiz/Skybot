using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Skybot.Api.Services.IntentsServices;
using Skybot.Api.Services.Settings;

namespace Skybot.Api.UnitTests.Services.IntentsServices
{
    [TestClass]
    public class IntentFactoryTests
    {
        [TestMethod]
        public void CreateIntent_ReturnsTranslateIntent_WhenNameIsTranslate()
        {
            var settingsMock = new Mock<ISettings>();

            var intentFactory = new IntentFactory(settingsMock.Object);

            var result = intentFactory.CreateIntent("TrAnslate");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TranslateIntent));
        }

        [TestMethod]
        public void CreateIntent_ReturnsNonIntent_WhenNameIsUnrecognized()
        {
            var settingsMock = new Mock<ISettings>();

            var intentFactory = new IntentFactory(settingsMock.Object);

            var result = intentFactory.CreateIntent("SomeIntent");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoneIntent));
        }

        [TestMethod]
        public void CreateIntent_ReturnsNonIntentByDefault()
        {
            var settingsMock = new Mock<ISettings>();

            var intentFactory = new IntentFactory(settingsMock.Object);

            var result = intentFactory.CreateIntent(string.Empty);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NoneIntent));
        }
    }
}
