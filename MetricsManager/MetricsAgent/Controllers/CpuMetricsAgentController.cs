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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;

        public CpuMetricsAgentController(ICpuMetricsRepository repository, ILogger<CpuMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик CPU (fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<CpuMetricsModel, CpuMetricsDto>());
            var m = config.CreateMapper();
            IList<CpuMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(fromTime, toTime);

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
    }
}

