using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/ram")]
    [ApiController]
    public class RamMeticsAgentController : ControllerBase
    {
        [HttpGet("available")]
        public IActionResult GetMetricsFromAgent()
        {
            return Ok();
        }
    }
}
