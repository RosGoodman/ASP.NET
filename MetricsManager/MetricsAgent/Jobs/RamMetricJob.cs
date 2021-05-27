using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class RamMetricJob : IJob
    {
        private IRamMetricsRepository _repository;
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int avalibleMBytes = Convert.ToInt32(_ramCounter.NextValue());
            DateTimeOffset time = DateTimeOffset.UtcNow;
            _repository.Create(new RamMetricsModel { Time = time, Value = avalibleMBytes });

            return Task.CompletedTask;
        }
    }
}
