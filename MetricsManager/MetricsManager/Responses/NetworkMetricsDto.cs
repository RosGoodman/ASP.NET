using System;
using System.Collections.Generic;

namespace MetricsManager.Responses
{
    public class NetworkMetricsDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
}
