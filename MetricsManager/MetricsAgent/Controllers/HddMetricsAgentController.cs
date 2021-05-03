using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/mtrics/hdd")]
    [ApiController]
    public class HddMetricsAgentController : ControllerBase
    {
        [HttpGet("left")]
        public IActionResult GetMetricsFromAgent()
        {
            return Ok();
        }
    }
}
