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
        private readonly IMapper _mapper;

        public DotNetMetricsController(IDotNetMetricsRepository repository, ILogger<DotNetMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик DotNet (agent Id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            IList<DotNetMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<DotNetMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}/recordNumb/{numb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long id, [FromRoute] long numb)
        {
            _logger.LogInformation($"Запрос на получение данных метрик DotNet (agent Id = {id}, record numb = {numb}).");

            DotNetMetricsModel metrics = _repository.GetByRecordNumb(id, numb);

            var response = new AllDotNetMetricsResponse()
            {
                Metrics = new List<DotNetMetricsDto>()
            };

            response.Metrics.Add(_mapper.Map<DotNetMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
