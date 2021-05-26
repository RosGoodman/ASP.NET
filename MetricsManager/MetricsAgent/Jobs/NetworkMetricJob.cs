using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private INetworkMetricsRepository _repository;
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", "Intel[R] I211 Gigabit Network Connection");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int bytesReceived = Convert.ToInt32(_networkCounter.NextValue());
            DateTimeOffset time = DateTimeOffset.UtcNow;
            _repository.Create(new NetworkMetricsModel { Time = time, Value = bytesReceived });

            return Task.CompletedTask;
        }
    }
}
