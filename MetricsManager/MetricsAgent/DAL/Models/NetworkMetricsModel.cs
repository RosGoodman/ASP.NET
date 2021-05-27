using System;

namespace MetricsAgent.Models
{
    public class NetworkMetricsModel
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
