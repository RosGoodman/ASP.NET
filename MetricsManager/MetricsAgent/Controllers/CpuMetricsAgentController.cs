using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;

        public CpuMetricsAgentController(ILogger<CpuMetricsAgentController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsAgentController");
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
            _logger.LogInformation("Привет! Это наше первое сообщение в лог.");
            return Ok("подключение есть");
        }

        [HttpGet("id/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик CPU (agentId = {id}, fromTime = {fromTime}, toTime = {toTime})");
            return Ok();
        }
    }
}

