using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;

        public NetworkMetricsAgentController(ILogger<NetworkMetricsAgentController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в NetworkMetricsAgentController");
        }

        [HttpGet("id/{Id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (agentId = {id}, fromTime = {fromTime}, toTime = {toTime})");
            return Ok();
        }
    }
}
