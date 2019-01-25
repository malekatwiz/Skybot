using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skybot.Api.Controllers;

namespace Skybot.Api.UnitTests.Controllers
{
    [TestClass]
    public class StatusControllerTests
    {
        [TestMethod]
        public void Index_ReturnsOkResult()
        {
            var statusController = new StatusController();

            var result = statusController.Index();
            var okObjectResult = result as OkObjectResult;

            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }
    }
}
