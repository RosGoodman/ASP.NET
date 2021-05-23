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
        private readonly IMapper _mapper;

        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (agent Id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            IList<RamMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<RamMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        [HttpGet("agentId/{id}/recordNumb/{numb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long id, [FromRoute] long numb)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (agent Id = {id}, record numb = {numb}).");

            RamMetricsModel metrics = _repository.GetByRecordNumb(id, numb);

            var response = new AllRamMetricsResponse()
            {
                Metrics = new List<RamMetricsDto>()
            };

            response.Metrics.Add(_mapper.Map<RamMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
