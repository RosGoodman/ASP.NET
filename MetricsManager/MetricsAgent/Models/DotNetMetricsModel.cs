using System;

namespace MetricsAgent.Models
{
    public class DotNetMetricsModel
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public DateTimeOffset DateTime { get; set; }
    }
}
