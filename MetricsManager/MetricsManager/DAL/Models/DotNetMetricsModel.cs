using System;

namespace MetricsManager.Models
{
    public class DotNetMetricsModel
    {
        public int AgentId { get; set; }

        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
