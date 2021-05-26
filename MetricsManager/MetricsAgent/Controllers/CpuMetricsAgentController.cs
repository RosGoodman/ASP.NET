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
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsAgentController : ControllerBase
    {
        private readonly ILogger<CpuMetricsAgentController> _logger;
        private readonly ICpuMetricsRepository _repository;
        private readonly IMapper _mapper;

        public CpuMetricsAgentController(ICpuMetricsRepository repository, ILogger<CpuMetricsAgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>Приветствие. Простой зарос для проверки подключения.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET greeting
        /// </remarks>
        /// <returns>Возвращает приветствие.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("greeting")]
        public IActionResult GreetingMethod()
        {
            return Ok("Привет. Программа запущена!");
        }

        /// <summary>Получает мерики CPU на заданном диапазоне времени.</summary>
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
            _logger.LogInformation($"Запрос на получение метрик CPU (" +
                $"fromTime = {request.FromTime:u}," +
                $" toTime = {request.ToTime:u})");

            IList<CpuMetricsModel> metrics = _repository.GetMetricsFromeTimeToTime(request.FromTime, request.ToTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(_mapper.Map<CpuMetricsDto>(metric));
            }

            return Ok(metrics);
        }
    }
}

