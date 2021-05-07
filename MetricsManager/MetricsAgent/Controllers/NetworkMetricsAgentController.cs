using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

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

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricsModel, NetworkMetricsDto>());
            var m = config.CreateMapper();
            IList<NetworkMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(id, fromTime, toTime);

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

        [HttpGet("all")]
        public IActionResult GetMetrics()
        {
            _logger.LogInformation($"Запрос на получение метрик Network");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetricsModel, NetworkMetricsDto>());
            var m = config.CreateMapper();
            IList<NetworkMetricsModel> metrics = _repository.GetAll();

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                // добавляем объекты в ответ при помощи мапера
                response.Metrics.Add(m.Map<NetworkMetricsDto>(metric));
            }

            return Ok(response);
        }
    }
}
