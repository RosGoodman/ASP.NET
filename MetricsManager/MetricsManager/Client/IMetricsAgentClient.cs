using MetricsManager.Client.ApiResponses;
using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Responses;
using System.Collections.Generic;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsResponse GetAllHddMetricsAsync(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsResponse GetAllDonNetMetrics(GetAllDotNetMetricsApiRequest request);

        AllCpuMetricsResponse GetAllCpuMetricsAsync(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
    }
}
