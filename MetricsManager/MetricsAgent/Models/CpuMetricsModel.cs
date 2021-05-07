using System;

namespace MetricsAgent.Models
{
    public class CpuMetricsModel
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
