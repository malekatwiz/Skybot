﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skybot.Api.Models;
using Skybot.Api.Services;

namespace Skybot.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SkybotController : ControllerBase
    {
        private readonly IRecognitionService _recognitionService;
        private readonly ILogger _logger;

        public SkybotController(IRecognitionService recognitionService, ILogger<SkybotController> logger)
        {
            _recognitionService = recognitionService;
            _logger = logger;
        }

        [Route("process")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Process([FromBody]QueryModel model)
        {
            _logger.LogInformation($"Received new request to process: {model?.Query}");

            var result = await _recognitionService.Process(model?.Query);
            if (result != null)
            {
                return Ok(result.Message);
            }

            _logger.LogInformation("Failed to process incoming request");
            return new BadRequestResult();
        }
    }
}
