using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsAgentController(INetworkMetricsRepository repository, ILogger<NetworkMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("id/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var metrics = _repository.GetMetricsFromeTimeToTime(id, fromTime, toTime);
            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetrics()
        {
            _logger.LogInformation($"Запрос на получение метрик Network");
            var metrics = _repository.GetAll();
            return Ok(metrics);
        }
    }
}
