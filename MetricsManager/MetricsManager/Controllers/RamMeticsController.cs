using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/mtrics/ram")]
    [ApiController]
    public class RamMeticsController : ControllerBase
    {
        private readonly ILogger<RamMeticsController> _logger;

        public RamMeticsController(ILogger<RamMeticsController> logger)
        {
            _logger = logger;
            logger.LogDebug(1, "NLog встроен в RamMeticsController");
        }

        [HttpGet("available")]
        public IActionResult GetMetricsFromAgent([FromRoute] int Id)
        {
            _logger.LogInformation($"Запрос на получение метрик RAM (agentID = {Id}");
            return Ok();
        }
    }
}
