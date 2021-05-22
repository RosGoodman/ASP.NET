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
            var fromParameter = request.FromTime.ToString("u");
            var toParameter = request.ToTime.ToString("u");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/hdd/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                AllHddMetricsResponse allMetricsResponse = new();
                allMetricsResponse.Metrics = JsonSerializer.Deserialize<List<HddMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return allMetricsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllCpuMetricsResponse GetAllCpuMetricsAsync(GetAllCpuMetricsApiRequest request)
        {
            string fromParameter = request.FromTime.ToString("u");
            string toParameter = request.ToTime.ToString("u");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/cpu/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                AllCpuMetricsResponse allMetricsResponse = new();
                allMetricsResponse.Metrics = JsonSerializer.Deserialize<List<CpuMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return allMetricsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllDotNetMetricsResponse GetAllDonNetMetrics(GetAllDotNetMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("u");
            var toParameter = request.ToTime.ToString("u");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/dotnet/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                AllDotNetMetricsResponse allMetricsResponse = new();
                allMetricsResponse.Metrics = JsonSerializer.Deserialize<List<DotNetMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return allMetricsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllNetworkMetricsResponse GetAllNetworkMetrics(GetAllNetworkMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("u");
            var toParameter = request.ToTime.ToString("u");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/network/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                AllNetworkMetricsResponse allMetricsResponse = new();
                allMetricsResponse.Metrics = JsonSerializer.Deserialize<List<NetworkMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return allMetricsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public AllRamMetricsResponse GetAllRamMetrics(GetAllRamMetricsApiRequest request)
        {
            var fromParameter = request.FromTime.ToString("u");
            var toParameter = request.ToTime.ToString("u");

            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"http://{request.ClientBaseAddress}/api/metrics/ram/from/{fromParameter}/to/{toParameter}");
            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                using var streamReader = new StreamReader(responseStream);
                var values = streamReader.ReadToEnd();

                AllRamMetricsResponse allMetricsResponse = new();
                allMetricsResponse.Metrics = JsonSerializer.Deserialize<List<RamMetricsDto>>(values, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return allMetricsResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
