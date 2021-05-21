using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private IHddMetricsRepository _repository;
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
            _hddCounter = new PerformanceCounter("LogicalDisk", "Free Megabytes", "C:");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int freeSpace = Convert.ToInt32(_hddCounter.NextValue());
            DateTimeOffset time = DateTimeOffset.UtcNow;
            _repository.Create(new HddMetricsModel { Time = time, Value = freeSpace });

            return Task.CompletedTask;
        }
    }
}
