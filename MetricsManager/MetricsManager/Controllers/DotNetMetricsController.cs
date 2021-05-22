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
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private readonly ILogger<DotNetMetricsController> _logger;
        private readonly IDotNetMetricsRepository _repository;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик DotNet (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetricsModel, DotNetMetricsDto>());
            var m = config.CreateMapper();
            IList<DotNetMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<DotNetMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение данных метрик DotNet (id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<DotNetMetricsModel, DotNetMetricsDto>());
            var m = config.CreateMapper();
            DotNetMetricsModel metrics = _repository.GetById(id);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            response.Metrics.Add(m.Map<DotNetMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
