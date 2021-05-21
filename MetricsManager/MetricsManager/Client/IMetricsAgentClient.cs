using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsResponse GetAllDonNetMetrics(GetAllDotNetMetricsApiRequest request);

        AllCpuMetricsResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
    }
}
