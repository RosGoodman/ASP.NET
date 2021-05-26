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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>Получает мерики Network на заданном диапазоне времени.</summary>
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
            _logger.LogInformation($"Запрос на получение метрик Network (agent Id = {id}, fromTime = {fromTime}, toTime = {toTime})");

            IList<NetworkMetricsModel> metrics = _repository.GetMetricsFromeTimeToTimeFromAgent(id, fromTime, toTime);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<NetworkMetricsDto>(metric));
            }

            return Ok(metrics);
        }

        /// <summary>Получает указанную запись метрики Network выбранного агента.</summary>
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
            _logger.LogInformation($"Запрос на получение метрик Network (agent Id = {id}, record numb = {numb}).");

            NetworkMetricsModel metrics = _repository.GetByRecordNumb(id, numb);

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricsDto>()
            };

            response.Metrics.Add(_mapper.Map<NetworkMetricsDto>(metrics));

            return Ok(metrics);
        }
    }
}
