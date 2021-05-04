using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly ILogger<AgentController> _logger;

        public AgentController(ILogger<AgentController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в AgentController");
        }

        [HttpGet("read")]
        public IActionResult GetAllAgents()
        {
            _logger.LogInformation($"Запрос на получение всех агентов.");
            return Ok();
        }
    }
}
