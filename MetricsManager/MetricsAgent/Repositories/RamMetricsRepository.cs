using MetricsAgent.Models;
using System.Collections.Generic;

namespace MetricsAgent.Repositories
{
    public class RamMetricsRepository
    {
        //тут пока пустые методы и заглушки

        private List<HddMetricsModel> _ramMetrics;
        public RamMetricsRepository()
        {
            _ramMetrics = new List<HddMetricsModel>();
        }

        public void Create(HddMetricsModel model)
        {
            _ramMetrics.Add(model);
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTime()
        {
            return _ramMetrics;
        }

        public void Delete(int id)
        {

        }
    }
}
