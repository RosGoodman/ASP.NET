using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetricsModel>
    {
        List<RamMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private static SQLiteConnection _connection;

        public void Create(RamMetricsModel model)
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO rammetrics(value, time) VALUES({model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<RamMetricsModel> GetAll()
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM rammetrics";

            var returnList = new List<RamMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetricsModel
                    {
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }

                return returnList;
            }
        }

        public List<RamMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM rammetrics WHERE time > {from} AND time < {to} AND id = {id}";
            var returnList = new List<RamMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }

            return returnList;
        }
    }
}
