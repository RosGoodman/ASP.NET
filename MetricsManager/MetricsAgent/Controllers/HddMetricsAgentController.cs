using AutoMapper;
using MetricsAgent.Controllers.Requests;
using MetricsAgent.Models;
using MetricsAgent.Repositories;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;
        private readonly IHddMetricsRepository _repository;

        public HddMetricsAgentController(IHddMetricsRepository repository, ILogger<HddMetricsAgentController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] MetricsRequest request)
        {
            _logger.LogInformation($"Запрос на получение метрик HDD (" +
                $"fromTime = {request.FromTime:u}," +
                $" toTime = {request.ToTime:u})");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetricsModel, HddMetricsDto>());
            var m = config.CreateMapper();
            IList<HddMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(request.FromTime, request.ToTime);

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
    }
}
