using System;

namespace MetricsManager.Models
{
    public class DotNetMetricsModel
    {
        public int AgentId { get; set; }

        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
