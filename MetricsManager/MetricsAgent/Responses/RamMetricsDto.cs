using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class RamMetricsDto
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllRamMetricsResponse
    {
        public List<RamMetricsDto> Metrics { get; set; }
    }
}
