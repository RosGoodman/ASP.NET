using System;

namespace MetricsManager.Models
{
    public class RamMetricsModel
    {
        public int AgentId { get; set; }

        public int Id { get; set; }

        public double Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
