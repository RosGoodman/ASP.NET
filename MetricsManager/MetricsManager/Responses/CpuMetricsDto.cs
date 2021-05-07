using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class CpuMetricsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllCpuMetricsResponse
    {
        public List<CpuMetricsDto> Metrics { get; set; }
    }
}
