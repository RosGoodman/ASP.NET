using System;

namespace MetricsManager.Models
{
    public class CpuMetricsModel
    {
        public long AgentId { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
