using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class DotNetMetricsDto
    {
        public int Id { get; set; }
        public long Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllDotNetMetricsResponse
    {
        public List<DotNetMetricsDto> Metrics { get; set; }
    }
}
