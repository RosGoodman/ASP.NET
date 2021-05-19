using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class DotNetMetricsDto
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricsDto> Metrics { get; set; }
    }
}
