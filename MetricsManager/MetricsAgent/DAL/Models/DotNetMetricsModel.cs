using System;

namespace MetricsAgent.Models
{
    public class DotNetMetricsModel
    {
        public long Value { get; set; }

        public DateTimeOffset Time { get; set; }
    }
}
