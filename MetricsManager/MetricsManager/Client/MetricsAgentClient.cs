using MetricsManager.Client.ApiResponses;
using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Models;
using MetricsManager.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
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

        public AllHddMetricsResponse GetAllHddMetricsAsync(GetAllHddMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToUnixTimeSeconds();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/hdd/from/{fromParameter}/to/{toParameter}");
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

        public AllCpuMetricsResponse GetAllCpuMetricsAsync(GetAllCpuMetricsApiRequest request)
        {
            string fromParameter = request.FromTime.ToString("yyyy-MM-dd HH-mm-ss");
            string toParameter = request.ToTime.ToString("yyyy-MM-dd HH-mm-ss");
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;

                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                var result = JsonSerializer.Deserialize<IList<CpuMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                List<CpuMetricsDto> newMetricsList = new();

                foreach (var metric in result)
                {
                    var newMetric = new CpuMetricsDto { Time = metric.Time, Value = metric.Value };
                    newMetricsList.Add(newMetric);
                }

                AllCpuMetricsResponse allCpuMetricsResponse = new();
                allCpuMetricsResponse.Metrics = newMetricsList;

                return allCpuMetricsResponse;
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
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/dotnet/from/{fromParameter}/to/{toParameter}");
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
            var fromParameter = request.FromTime.ToUniversalTime();
            var toParameter = request.ToTime.ToUnixTimeSeconds();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
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
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/ram/from/{fromParameter}/to/{toParameter}");
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
