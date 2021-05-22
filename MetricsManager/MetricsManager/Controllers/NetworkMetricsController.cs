using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Repositories;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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
            _logger.LogInformation($"Запрос на получение метрик Network (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricsModel, NetworkMetricsDto>());
            var m = config.CreateMapper();
            IList<NetworkMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<NetworkMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricsModel, NetworkMetricsDto>());
            var m = config.CreateMapper();
            NetworkMetricsModel metrics = _repository.GetById(id);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            response.Metrics.Add(m.Map<NetworkMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
