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
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;
        private readonly IRamMetricsRepository _repository;

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricsModel, RamMetricsDto>());
            var m = config.CreateMapper();
            IList<RamMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<RamMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, RamMetricsDto>());
            var m = config.CreateMapper();
            RamMetricsModel metrics = _repository.GetById(id);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            response.Metrics.Add(m.Map<RamMetricsDto>(metrics));

            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetricsAll()
        {
            _logger.LogInformation($"Запрос на получение метрик RAM");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricsModel, RamMetricsDto>());
            var m = config.CreateMapper();
            IList<RamMetricsModel> metrics = _repository.GetAll();

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<RamMetricsDto>(metric));
            }

            return Ok(response);
        }
    }
}
