using Dapper;
using MetricsAgent.DAL;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsAgent.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetricsModel>
    {
        List<RamMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
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
                connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = model.Value,
                        time = model.Time
                    });
            }
        }

        public List<RamMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<RamMetricsModel>($"SELECT time, value From rammetrics WHERE time >= @fromTime AND time <= @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
    }
}
