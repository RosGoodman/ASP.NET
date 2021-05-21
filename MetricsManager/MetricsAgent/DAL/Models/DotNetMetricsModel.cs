using System;

namespace MetricsAgent.Models
{
    public class DotNetMetricsModel
    {
        public int Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
