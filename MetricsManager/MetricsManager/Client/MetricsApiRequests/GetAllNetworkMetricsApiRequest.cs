using System;

namespace MetricsManager.Client.MetricsApiRequests
{
    public class GetAllNetworkMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}
