using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public interface ICpuMetricsRepository : IRepository<CpuMetricsModel>
    {
        List<CpuMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private static SQLiteConnection _connection;

        public void Create(CpuMetricsModel model)
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO cpumetrics(value, time) VALUES({model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetricsModel> GetAll()
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM cpumetrics";

            var returnList = new List<CpuMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetricsModel
                    {
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }

                return returnList;
            }
        }

        public List<CpuMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM cpumetrics WHERE time > {from} AND time < {to} AND id = {id}";
            var returnList = new List<CpuMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
            }

            return returnList;
        }
    }
}
