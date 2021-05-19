using System;

namespace MetricsAgent.Models
{
    public class CpuMetricsModel
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
