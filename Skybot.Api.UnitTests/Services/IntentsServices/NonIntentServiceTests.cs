using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybot.Api.Services.IntentsServices;

namespace Skybot.Api.UnitTests.Services.IntentsServices
{
    [TestClass]
    public class NonIntentServiceTests
    {
        [TestMethod]
        public async Task Execute_ReturnsRecognitionResultWithErrorMessage()
        {
            var nonIntent = new NonIntent();

            var result = await nonIntent.Execute(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Message, "Sorry, I don't understand what you asked me");
        }
    }
}
