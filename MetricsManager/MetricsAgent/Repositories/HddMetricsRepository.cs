using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public interface IHddMetricsRepository : IRepository<HddMetricsModel>
    {
        List<HddMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private SQLiteConnection _connection;

        public void Create(HddMetricsModel model)
        {
            _connection = new SQLiteConnection();
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO hddmetrics(value, time) VALUES({model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<HddMetricsModel> GetAll()
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics";

            var returnList = new List<HddMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricsModel
                    {
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
                _connection.Dispose();

                return returnList;
            }
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE time > {from} AND time < {to} AND id = {id}";
            var returnList = new List<HddMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricsModel
                    {
                        Id = reader.GetInt32(0),
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(2))
                    });
                }
            }
            _connection.Dispose();

            return returnList;
        }
    }
}
