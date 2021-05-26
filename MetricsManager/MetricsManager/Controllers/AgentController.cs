using AutoMapper;
using MetricsManager.Models;
using MetricsManager.Repositories;
using MetricsManager.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MetricsManager.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;
        private readonly IAgentRepository _repository;
        private readonly IMapper _mapper;

        public AgentController(IAgentRepository repository, ILogger<AgentController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в AgentController");
            _mapper = mapper;
        }

        /// <summary>Получить список всех агентов.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET all
        /// </remarks>
        /// <returns>Список всех зарегестрированных агентов.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Запрос на получение метрик CPU");

            var metrics = _repository.GetAll();

            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentsDto>()
            };

            foreach (var metric in metrics)
            {
                // добавляем объекты в ответ при помощи мапера
                response.Metrics.Add(_mapper.Map<AgentsDto>(metric));
            }

            return Ok(response);
        }

        /// <summary>Получить данные выбранного агента.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     GET AgentId/3
        /// </remarks>
        /// <param name="id">ID агента.</param>
        /// <returns>Данные выбранного агента.</returns>
        /// <response code="201">Если все хорошо.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpGet("AgentId/{id}")]
        public IActionResult GetAgentById([FromRoute]int id)
        {
            _logger.LogInformation($"Запрос на получение данных агента (agent Id = {id}).");

            AgentModel metrics = _repository.GetById(id);

            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentsDto>()
            };
            
            response.Metrics.Add(_mapper.Map<AgentsDto>(metrics));

            return Ok(metrics);
        }

        /// <summary>Зарегестрировать нового агента.</summary>
        /// <remarks>
        /// Пример запроса:
        ///     POST name/agent1/address/localhost:61977
        /// </remarks>
        /// <param name="name">Имя нового агента.</param>
        /// <param name="address">Адрес нового агента.</param>
        /// <response code="200">Запрос выполнен успешно.</response>
        /// <response code="400">Если передали не правильные параметры.</response>
        [HttpPost("name/{name}/address/{address}")]
        public IActionResult CreateAgent([FromRoute] string name, [FromRoute] string address)
        {
            _logger.LogInformation($"Запрос на создание нового агента (name = {name}, address = {address}).");

            AgentModel newAgent = new AgentModel { Name = name, Address = address };
            _repository.Create(newAgent);
            return Ok();
        }
    }
}
