using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybot.Api.Controllers;
using Moq;
using Skybot.Api.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skybot.Api.Models;

namespace Skybot.Api.UnitTests.Controllers
{
    [TestClass]
    public class SkybotControllerTests
    {
        [TestMethod]
        public async Task Process_ReturnsOkObjectResult_WhenQueryIsNotEmpty()
        {
            var query = "I'm testing you";
            var recognitionResult = new RecognitionResult
            {
                Message = "I will pass your test"
            };

            var recognitionServiceMock = new Mock<IRecognitionService>();
            recognitionServiceMock.Setup(x => x.Process(query))
                .Returns(Task.FromResult(recognitionResult))
                .Verifiable();

            var loggerMock = new Mock<ILogger<SkybotController>>();

            var skybotController = new SkybotController(recognitionServiceMock.Object,
                loggerMock.Object);

            var result = await skybotController.Process(query);
            var okObjectResult = result as OkObjectResult;

            recognitionServiceMock.Verify();

            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(okObjectResult.StatusCode, 200);
            Assert.AreEqual(okObjectResult.Value, recognitionResult.Message);
        }

        [TestMethod]
        public async Task Process_ReturnsHttpBadRequest_WhenQueryIsEmpty()
        {
            var recognitionServiceMock = new Mock<IRecognitionService>();
            recognitionServiceMock.Setup(x => x.Process(string.Empty))
                .Returns(Task.FromResult((RecognitionResult)null))
                .Verifiable();

            var loggerMock = new Mock<ILogger<SkybotController>>();

            var skybotController = new SkybotController(recognitionServiceMock.Object,
                loggerMock.Object);

            var result = await skybotController.Process(string.Empty);
            var badRequestResult = result as BadRequestResult;

            recognitionServiceMock.Verify();

            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(badRequestResult.StatusCode, 400);
        }
    }
}
