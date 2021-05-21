using MetricsAgent.Models;
using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private IDotNetMetricsRepository _repository;
        private PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
        }

        public Task Execute(IJobExecutionContext context)
        {
            int value = Convert.ToInt32(_dotNetCounter.NextValue());
            DateTimeOffset time = DateTimeOffset.UtcNow;
            _repository.Create(new DotNetMetricsModel { Time = time, Value = value });

            return Task.CompletedTask;
        }
    }
}
