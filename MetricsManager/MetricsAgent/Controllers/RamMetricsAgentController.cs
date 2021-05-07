using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsAgentController : ControllerBase
    {
        private readonly ILogger<RamMetricsAgentController> _logger;
        private readonly IRamMetricsRepository _repository;

        public RamMetricsAgentController(IRamMetricsRepository repository, ILogger<RamMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("id/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var metrics = _repository.GetMetricsFromeTimeToTime(id, fromTime, toTime);
            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetrics()
        {
            _logger.LogInformation($"Запрос на получение метрик RAM");
            var metrics = _repository.GetAll();
            return Ok(metrics);
        }
    }
}
