using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    public interface IHddMetricsRepository : IRepository<HddMetricsModel>
    {
        List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public HddMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(HddMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO hddmetrics(AgentId, value, time) VALUES(@AgentId, @value, @time)",
                    new
                    {
                        AgentId = model.AgentId,
                        value = model.Value,
                        time = model.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<HddMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricsModel>("SELECT * FROM hddmetrics").ToList();
        }

        public HddMetricsModel GetByRecordNumb(long id, long recordNumb)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<HddMetricsModel>("SELECT * FROM hddmetrics WHERE agentid = @agentid AND id = @id", new
                {
                    AgentId = id,
                    id = recordNumb
                });
            }
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long agentid, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricsModel>($"SELECT * From hddmetrics WHERE time >= @fromTime AND time <= @toTime And agentid = @agentid",
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
            var result = connection.QueryFirstOrDefault<HddMetricsModel>("SELECT max(time) FROM hddmetrics WHERE agentid = @agentid",
                new
                {
                    agentid = agentId
                });
            return (result ?? new HddMetricsModel()).Time;
        }
    }
}
