using MetricsAgent.Repositories;
using Quartz;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MetricsAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private ICpuMetricsRepository _repository;

        // счетчик для метрики CPU
        private PerformanceCounter _cpuCounter;

        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            DateTimeOffset time = DateTimeOffset.UtcNow;

            // теперь можно записать что-то при помощи репозитория
            _repository.Create(new Models.CpuMetricsModel { Time = time, Value = cpuUsageInPercents });

            return Task.CompletedTask;
        }
    }
}
