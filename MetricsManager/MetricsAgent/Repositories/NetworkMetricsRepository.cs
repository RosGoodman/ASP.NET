using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsAgent.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricsModel>
    {
        List<NetworkMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private static SQLiteConnection _connection;

        public void Create(NetworkMetricsModel model)
        {
            DBConnectionOpen();

            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO networkmetrics(value, time) VALUES({model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<NetworkMetricsModel> GetAll()
        {
            DBConnectionOpen();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics";

            var returnList = new List<NetworkMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricsModel
                    {
                        Value = reader.GetInt32(1),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(2))
                    });
                }
                _connection.Dispose();

                return returnList;
            }
        }

        public List<NetworkMetricsModel> GetMetricsFromeTimeToTime(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            DBConnectionOpen();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE time > {from} AND time < {to} AND id = {id}";
            var returnList = new List<NetworkMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricsModel
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

        private static void DBConnectionOpen()
        {
            const string connectionString = "Data Source=Metrics.db";
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }
    }
}
