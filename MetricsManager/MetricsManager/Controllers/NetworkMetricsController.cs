using MetricsManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (agentID = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);
            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (agentID = {id})");

            var metrics = _repository.GetById(id);
            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetricsAll()
        {
            _logger.LogInformation($"Запрос на получение метрик Network всех агентов");

            var metrics = _repository.GetAll();
            return Ok(metrics);
        }
    }
}
