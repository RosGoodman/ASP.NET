using System;

namespace MetricsAgent.Models
{
    public class NetworkMetricsModel
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
