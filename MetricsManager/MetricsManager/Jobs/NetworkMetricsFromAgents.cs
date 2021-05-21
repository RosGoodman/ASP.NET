﻿using MetricsManager.Client;
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
    public class NetworkMetricsFromAgents : IJob
    {
        private INetworkMetricsRepository _repository;
        private IAgentRepository _agentRepository;
        private IMetricsAgentClient _client;

        public NetworkMetricsFromAgents(INetworkMetricsRepository repository, IAgentRepository agentRepository, IMetricsAgentClient client)
        {
            _repository = repository;
            _agentRepository = agentRepository;
            _client = client;
        }

        public Task Execute(IJobExecutionContext context)
        {
            IList<AgentModel> agents = _agentRepository.GetAll();
            DateTimeOffset fromTime = _repository.GetLastTime();
            DateTimeOffset toTime = DateTimeOffset.UtcNow;

            foreach (var agent in agents)
            {
                AllNetworkMetricsResponse allMetrics = _client.GetAllNetworkMetrics(new GetAllNetworkMetricsApiRequest
                {
                    FromTime = fromTime,
                    ToTime = toTime,
                    ClientBaseAddress = agent.Address
                });

                if (allMetrics != null)
                {
                    foreach (var metric in allMetrics.Metrics)
                    {
                        _repository.Create(new NetworkMetricsModel
                        {
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
