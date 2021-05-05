using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/ram")]
    [ApiController]
    public class RamMeticsAgentController : ControllerBase
    {
        private readonly ILogger<RamMeticsAgentController> _logger;

        public RamMeticsAgentController(ILogger<RamMeticsAgentController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в RamMeticsAgentController");
        }

        [HttpGet("available/id/{Id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (agentId = {id})");
            return Ok();
        }
    }
}
