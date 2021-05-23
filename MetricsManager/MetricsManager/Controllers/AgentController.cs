﻿using AutoMapper;
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

        public AgentController(IAgentRepository repository, ILogger<AgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в AgentController");
        }

        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Запрос на получение метрик CPU");

            // задаем конфигурацию для мапера. Первый обобщенный параметр -- тип объекта
            // источника, второй -- тип объекта в который перетекут данные из источника
            var config = new MapperConfiguration(cfg => cfg.CreateMap<AgentModel, AgentsDto>());
            var m = config.CreateMapper();
            var metrics = _repository.GetAll();

            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentsDto>()
            };

            foreach (var metric in metrics)
            {
                // добавляем объекты в ответ при помощи мапера
                response.Metrics.Add(m.Map<AgentsDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("AgentId/{id}")]
        public IActionResult GetAgentById([FromRoute]int id)
        {
            _logger.LogInformation($"Запрос на получение данных агента (agent Id = {id}).");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<AgentModel, AgentsDto>());
            var m = config.CreateMapper();
            AgentModel metrics = _repository.GetById(id);

            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentsDto>()
            };
            
            response.Metrics.Add(m.Map<AgentsDto>(metrics));

            return Ok(metrics);
        }

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
