using System;

namespace MetricsAgent.Models
{
    public class NetworkMetricsModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
