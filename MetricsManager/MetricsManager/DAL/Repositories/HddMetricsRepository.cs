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
        List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
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
                connection.Execute("INSERT INTO hddmetrics(Id, value, time) VALUES(@Id, @value, @time)",
                    new
                    {
                        Id = model.Id,
                        value = model.Value,
                        time = model.Time
                    });
            }
        }

        public IList<HddMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricsModel>("SELECT * FROM hddmetrics").ToList();
        }

        public HddMetricsModel GetById(int id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QuerySingle<HddMetricsModel>("SELECT * FROM hddmetrics WHERE id = @id",
                new
                {
                    id = id
                });
            }
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<HddMetricsModel>($"SELECT * From hddmetrics WHERE time > @fromTime AND time < @toTime And id = @id",
                    new
                    {
                        id = id,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }
    }
}
