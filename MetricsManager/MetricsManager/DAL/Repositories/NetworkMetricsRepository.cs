using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricsModel>
    {
        List<NetworkMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public NetworkMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(NetworkMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO networkmetrics(AgentId, value, time) VALUES(@AgentId, @value, @time)",
                    new
                    {
                        AgentId = model.AgentId,
                        value = model.Value,
                        time = model.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<NetworkMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<NetworkMetricsModel>("SELECT * FROM networkmetrics").ToList();
        }

        public NetworkMetricsModel GetByRecordNumb(long id, long recordNumb)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<NetworkMetricsModel>("SELECT * FROM networkmetrics WHERE agentid = @agentid AND id = @id", new
                {
                    AgentId = id,
                    id = recordNumb
                });
            }
        }

        public List<NetworkMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long agentid, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<NetworkMetricsModel>($"SELECT * From networkmetrics WHERE time >= @fromTime AND time <= @toTime And agentid = @agentid",
                    new
                    {
                        agentid = agentid,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
        public DateTimeOffset GetLastTime(long agentId)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.QueryFirstOrDefault<NetworkMetricsModel>("SELECT max(time) FROM networkmetrics WHERE agentid = @agentid",
                new
                {
                    agentid = agentId
                });
            return (result ?? new NetworkMetricsModel()).Time;
        }
    }
}
