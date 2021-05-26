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
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>Получает мерики HDD на заданном диапазоне времени.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET agentId/1/from/2019-01-01/to/2022-01-12
        /// </remarks>
        /// <param name="id">ID агента.</param>
        /// <param name="fromTime">Начальная метка времени.</param>
        /// <param name="toTime">Конечная метка времени.</param>
        /// <returns>Список мерик, которые сохранены в заданном диапазоне времени.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("agentId/{id}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id, [FromRoute] DateTimeOffset fromTime, [FromRoute] DateTimeOffset toTime)
        {
            _logger.LogInformation($"Запрос на получение метрик HDD (agent Id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            IList<HddMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<HddMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        /// <summary>Получает указанную запись метрики HDD выбранного агента.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET agentId/1/recorNumb/23
        /// </remarks>
        /// <param name="id">ID агента.</param>
        /// <param name="numb">Номер записи.</param>
        /// <returns>Выбранная запись метрики.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("agentId/{id}/recordNumb/{numb}")]
        public IActionResult GetMetricsFromAgent([FromRoute] long id, [FromRoute] long numb)
        {
            _logger.LogInformation($"Запрос на получение данных метрик HDD (agent Id = {id}, record numb = {numb}).");

            HddMetricsModel metrics = _repository.GetByRecordNumb(id, numb);

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricsDto>()
            };

            response.Metrics.Add(_mapper.Map<HddMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
