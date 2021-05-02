using MetricsManager.Models;
using System;
using System.Collections.Generic;

namespace MetricsManager.Repositories
{
    public class AgentsRepository
    {
        //тут пока пустые методы и заглушки

        private List<AgentModel> _agents;
        public AgentsRepository()
        {
            _agents = new List<AgentModel>();
        }

        public void Create(AgentModel model)
        {
            _agents.Add(model);
        }

        public List<AgentModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return _agents;
        }

        public void Delete(int id)
        {

        }
    }
}
