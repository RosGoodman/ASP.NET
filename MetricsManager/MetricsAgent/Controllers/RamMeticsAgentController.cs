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

        [HttpGet("available")]
        public IActionResult GetMetricsFromAgent()
        {
            _logger.LogInformation($"Запрос на получение метрик RAM");
            return Ok();
        }
    }
}
