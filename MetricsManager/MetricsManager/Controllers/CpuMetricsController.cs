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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly ICpuMetricsRepository _repository;

        public CpuMetricsController(ICpuMetricsRepository repository, ILogger<CpuMetricsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик CPU (id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            var m = config.CreateMapper();
            IList<CpuMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<CpuMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик CPU (id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            var m = config.CreateMapper();
            CpuMetricsModel metrics = _repository.GetById(id);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            response.Metrics.Add(m.Map<CpuMetricsDto>(metrics));

            return Ok(metrics);
        }

        [HttpGet("all")]
        public IActionResult GetMetricsAll()
        {
            _logger.LogInformation($"Запрос на получение метрик CPU");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            var m = config.CreateMapper();
            IList<CpuMetricsModel> metrics = _repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<CpuMetricsDto>(metric));
            }

            return Ok(response);
        }
    }
}

