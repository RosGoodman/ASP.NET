using System;

namespace MetricsAgent.Models
{
    public class HddMetricsModel
    {
        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
