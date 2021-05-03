using MetricsAgent.Models;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repositories
{
    public class CpuMetricsRepository
    {
        //тут пока пустые методы и заглушки

        private List<CpuMetricsModel> _cpuMetrics;
        public CpuMetricsRepository()
        {
            _cpuMetrics = new List<CpuMetricsModel>();
        }

        public void Create(CpuMetricsModel model)
        {
            _cpuMetrics.Add(model);
        }

        public List<CpuMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return _cpuMetrics;
        }

        public void Delete(int id)
        {

        }
    }
}
