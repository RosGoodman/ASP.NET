using Microsoft.AspNetCore.Mvc;

namespace MetricsManager.Controllers
{
    [Route("api/agent")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        [HttpGet("read")]
        public IActionResult GetAllAgents()
        {
            return Ok();
        }
    }
}
