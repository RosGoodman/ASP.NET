using MetricsManager.Client;
using MetricsManager.Client.MetricsApiRequests;
using MetricsManager.Models;
using MetricsManager.Repositories;
using MetricsManager.Responses;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MetricsManager.Jobs
{
    public class CpuMetricsFromAgents : IJob
    {
        private ICpuMetricsRepository _repository;
        private IAgentRepository _agentRepository;
        private IMetricsAgentClient _client;

        public CpuMetricsFromAgents(ICpuMetricsRepository repository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _repository = repository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            IList<AgentModel> agents = _agentRepository.GetAll();
            DateTimeOffset fromTime;
            DateTimeOffset toTime = DateTimeOffset.UtcNow;

            foreach (var agent in agents)
            {
                fromTime = _repository.GetLastTime(agent.Id);
                AllCpuMetricsResponse allMetrics = _client.GetAllCpuMetricsAsync(new GetAllCpuMetricsApiRequest
                {
                    AgentId = agent.Id,
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agent.Address
                });

                if (allMetrics != null && allMetrics != null)
                {
                    foreach (var metric in allMetrics.Metrics)
                    {
                        _repository.Create(new CpuMetricsModel{
                            AgentId = agent.Id,
                            Value = metric.Value,
                            Time = metric.Time
                        });
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
