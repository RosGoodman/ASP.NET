using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetricsModel>
    {
        List<RamMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public RamMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(RamMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO rammetrics(AgentId, value, time) VALUES(@AgentId, @value, @time)",
                    new
                    {
                        AgentId = model.AgentId,
                        value = model.Value,
                        time = model.Time
                    });
            }
        }

        public IList<RamMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<RamMetricsModel>("SELECT * FROM rammetrics").ToList();
        }

        public RamMetricsModel GetByRecordNumb(long id, long numb)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<RamMetricsModel>("SELECT * FROM rammetrics WHERE AgentId = @id",
                new
                {
                    AgentId = id
                });
            }
        }

        public List<RamMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<RamMetricsModel>($"SELECT * From rammetrics WHERE time >= @fromTime AND time <= @toTime And AgentId = @id",
                    new
                    {
                        AgentId = id,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
        public DateTimeOffset GetLastTime()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.QueryFirstOrDefault<RamMetricsModel>("SELECT * FROM rammetrics ORDER BY time DESC LIMIT 1");
            return (result ?? new RamMetricsModel()).Time;
        }
    }
}
