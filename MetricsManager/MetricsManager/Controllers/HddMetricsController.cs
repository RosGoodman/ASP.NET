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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _repository;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик HDD (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricsModel, HddMetricsDto>());
            var m = config.CreateMapper();
            IList<HddMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<HddMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение данных метрик HDD (id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricsModel, HddMetricsDto>());
            var m = config.CreateMapper();
            HddMetricsModel metrics = _repository.GetById(id);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            response.Metrics.Add(m.Map<HddMetricsDto>(metrics));

            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetricsAll()
        {
            _logger.LogInformation($"Запрос на получение метрик HDD");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricsModel, HddMetricsDto>());
            var m = config.CreateMapper();
            IList<HddMetricsModel> metrics = _repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<HddMetricsDto>(metric));
            }

            return Ok(response);
        }
    }
}
