using System;

namespace MetricsManager.Client.MetricsApiRequests
{
    public class GetAllCpuMetricsApiRequest
    {
        public int AgentId { get; set; }
        public DateTimeOffset FromTime { get; set; }
        public DateTimeOffset ToTime { get; set; }
        public string ClientBaseAddress { get; set; }
    }
}
