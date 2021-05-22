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

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (fromTime = {fromTime}, toTime = {toTime})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<RamMetricsModel, RamMetricsDto>());
            var m = config.CreateMapper();
            IList<RamMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(fromTime, toTime);

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
    }
}
