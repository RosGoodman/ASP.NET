using MetricsManager.Client.ApiResponses;
using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Responses;

namespace MetricsManager.Client
{
    public interface IMetricsAgentClient
    {
        AllRamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request);

        AllHddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request);

        AllDotNetMetricsResponse GetAllDonNetMetrics(GetAllDotNetMetricsApiRequest request);

        AllCpuMetricsApiResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request);

        AllNetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request);
    }
}
