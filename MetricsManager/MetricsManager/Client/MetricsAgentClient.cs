using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManager.Client
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllHddMetricsResponse GetAllHddMetrics(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/hdd/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllHddMetricsResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllCpuMetricsResponse GetAllCpuMetrics(GetAllCpuMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllCpuMetricsResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllDotNetMetricsResponse GetAllDonNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/dotnet/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllDotNetMetricsResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllNetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllNetworkMetricsResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllRamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{request.ClientBaseAddress}/api/metrics/ram/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                return JsonSerializer.DeserializeAsync<AllRamMetricsResponse>(responseStream).Result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
