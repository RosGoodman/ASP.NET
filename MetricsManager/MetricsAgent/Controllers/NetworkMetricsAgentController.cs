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
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsAgentController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsAgentController> _logger;
        private readonly INetworkMetricsRepository _repository;
        private readonly IMapper _mapper;

        public NetworkMetricsAgentController(INetworkMetricsRepository repository, ILogger<NetworkMetricsAgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>Получает мерики Network на заданном диапазоне времени.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET from/2019-01-01/to/2022-01-12
        /// </remarks>
        /// <param name="from">Начальная метка времени.</param>
        /// <param name="to">Конечная метка времени.</param>
        /// <returns>Список мерик, которые сохранены в заданном диапазоне времени.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromTimeToTime([FromRoute] MetricsRequest request)
        {
            _logger.LogInformation($"Запрос на получение метрик Network (" +
                $"fromTime = {request.FromTime:u}," +
                $" toTime = {request.ToTime:u})");

            IList<NetworkMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(request.FromTime, request.ToTime);

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
    }
}
