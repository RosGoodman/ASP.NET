using MetricsManager.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repositories
{
    public class NetworkMetricsRepository
    {
        //тут пока пустые методы и заглушки

        private List<NetworkMetricsModel> _networkMetrics;
        public NetworkMetricsRepository()
        {
            _networkMetrics = new List<NetworkMetricsModel>();
        }

        public void Create(NetworkMetricsModel model)
        {
            _networkMetrics.Add(model);
        }

        public List<NetworkMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return _networkMetrics;
        }

        public void Delete(int id)
        {

        }
    }
}
