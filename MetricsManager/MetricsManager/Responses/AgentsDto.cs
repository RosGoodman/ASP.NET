using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class AgentsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AllAgentsResponse
    {
        public List<AgentsDto> Metrics { get; set; }
    }
}
