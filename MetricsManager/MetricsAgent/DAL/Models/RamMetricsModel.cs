using System;

namespace MetricsAgent.Models
{
    public class RamMetricsModel
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
