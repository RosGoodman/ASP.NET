using System;

namespace MetricsAgent.Models
{
    public class HddMetricsModel
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
