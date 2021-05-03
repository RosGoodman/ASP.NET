using System;

namespace MetricsAgent.Models
{
    public class RamMetricsModel
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
