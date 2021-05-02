using MetricsManager.Models;
using System.Collections.Generic;

namespace MetricsManager.Repositories
{
    public class HddMetricsRepository
    {
        //тут пока пустые методы и заглушки

        private List<HddMetricsModel> _hddMetrics;
        public HddMetricsRepository()
        {
            _hddMetrics = new List<HddMetricsModel>();
        }

        public void Create(HddMetricsModel model)
        {
            _hddMetrics.Add(model);
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTime()
        {
            return _hddMetrics;
        }

        public void Delete(int id)
        {

        }
    }
}
