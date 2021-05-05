using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        private readonly ILogger<HddMetricsAgentController> _logger;

        public HddMetricsAgentController(ILogger<HddMetricsAgentController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в HddMetricsAgentController");
        }

        [HttpGet("left/id/{Id}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int id)
        {
            _logger.LogInformation($"Запрос на получение метрик Hdd (agentId = {id})");
            return Ok();
        }
    }
}
