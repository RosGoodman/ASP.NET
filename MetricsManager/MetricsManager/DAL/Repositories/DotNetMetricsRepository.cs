using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricsModel>
    {
        List<DotNetMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public DotNetMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(DotNetMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO dotnetmetrics(AgentId, value, time) VALUES(@AgentId, @value, @time)",
                    new
                    {
                        AgentId = model.AgentId,
                        value = model.Value,
                        time = model.Time
                    });
            }
        }

        public IList<DotNetMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<DotNetMetricsModel>("SELECT * FROM dotnetmetrics").ToList();
        }

        public DotNetMetricsModel GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<DotNetMetricsModel>("SELECT * FROM dotnetmetrics WHERE id = @id",
                new
                {
                    id = id
                });
            }
        }

        public DateTimeOffset GetLastTime()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.QueryFirstOrDefault<DotNetMetricsModel>("SELECT * FROM dotnetmetrics ORDER BY time DESC LIMIT 1");
            return (result ?? new DotNetMetricsModel()).Time;
        }

        public List<DotNetMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<DotNetMetricsModel>($"SELECT * From dotnetmetrics WHERE time > @fromTime AND time < @toTime And id = @id",
                    new
                    {
                        id = id,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
    }
}
