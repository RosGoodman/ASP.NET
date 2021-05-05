using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/dotnet")]
    [ApiController]
    public class DotNetMetricsAgentController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsAgentController> _logger;

        public DotNetMetricsAgentController(ILogger<DotNetMetricsAgentController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в DotNetMetricsAgentController");
        }

        [HttpGet("errors-count/id/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик DotNet (agentId = {id}, fromTime = {fromTime}, toTime = {toTime})");
            return Ok();
        }
    }
}
