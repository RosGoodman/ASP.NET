using MetricsManager.Models;
using MetricsManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation($"Запрос на получение всех агентов.");

            var metrics = _repository.GetAll();
            return Ok(metrics);
        }

        [HttpGet("AgentId/{id}")]
        public IActionResult GetAgentById([FromRoute]int id)
        {
            _logger.LogInformation($"Запрос на получение данных агента (id = {id}).");

            var metrics = _repository.GetById(id);
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
