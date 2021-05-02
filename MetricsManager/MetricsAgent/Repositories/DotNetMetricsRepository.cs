using MetricsAgent.Models;
using System;
using System.Collections.Generic;

namespace MetricsAgent.Repositories
{
    public class DotNetMetricsRepository
    {
        //тут пока пустые методы и заглушки

        private List<DotNetMetricsModel> _dotNetMetrics;
        public DotNetMetricsRepository()
        {
            _dotNetMetrics = new List<DotNetMetricsModel>();
        }

        public void Create(DotNetMetricsModel model)
        {
            _dotNetMetrics.Add(model);
        }

        public List<DotNetMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return _dotNetMetrics;
        }

        public void Delete(int id)
        {

        }
    }
}
