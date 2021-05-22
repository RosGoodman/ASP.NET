using Dapper;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using MetricsAgent.DAL;

namespace MetricsAgent.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetricsModel>
    {
        IList<CpuMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)",
                    new
                    {
                        value = model.Value,
                        time = model.Time.ToUnixTimeSeconds()
                    });
            }
        }

        public IList<CpuMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<CpuMetricsModel>($"SELECT time, value From cpumetrics WHERE time > @fromTime AND time < @toTime",
                    new
                    {
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
    }
}
