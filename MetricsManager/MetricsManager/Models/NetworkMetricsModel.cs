using System;

namespace MetricsManager.Models
{
    public class NetworkMetricsModel
    {
        public int AgentId { get; set; }

        public int Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
