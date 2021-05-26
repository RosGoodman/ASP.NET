using System;
using System.Collections.Generic;

namespace MetricsAgent.Responses
{
    public class NetworkMetricsDto
    {
        public int Value { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public class AllNetworkMetricsResponse
    {
        public List<NetworkMetricsDto> Metrics { get; set; }
    }
}
