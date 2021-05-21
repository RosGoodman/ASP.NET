using System;

namespace MetricsManager.Client.MetricsApiRequests
{
    public class GetAllCpuMetricsApiRequest
    {
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}
