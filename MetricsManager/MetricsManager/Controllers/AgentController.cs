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

        public AgentController(IAgentRepository repository, ILogger<AgentController> logger)
        {
            _repository = repository;
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в AgentController");
        }

        [HttpGet("all")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Запрос на получение агентов");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<AgentModel, AgentsDto>());
            var m = config.CreateMapper();
            IList<AgentModel> metrics = _repository.GetAll();

            var response = new AllAgentsResponse()
            {
                Metrics = new List<AgentsDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(m.Map<AgentsDto>(metric));
            }

            return Ok(response);
        }

        [HttpGet("AgentId/{id}")]
        public IActionResult GetAgentById([FromRoute]int id)
        {
            _logger.LogInformation($"Запрос на получение данных агента (id = {id}).");

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

        [HttpPost("Name/{name}")]
        public IActionResult CreateAgent([FromRoute] string name)
        {
            _logger.LogInformation($"Запрос на создание нового агента (name = {name}).");

            AgentModel newAgent = new AgentModel { Name = name };
            _repository.Create(newAgent);
            return Ok();
        }
    }
}
