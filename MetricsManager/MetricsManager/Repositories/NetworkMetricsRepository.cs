using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    public interface INetworkMetricsRepository : IRepository<NetworkMetricsModel>
    {
        List<NetworkMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class NetworkMetricsRepository : INetworkMetricsRepository
    {
        private SQLiteConnection _connection;

        public void Create(NetworkMetricsModel model)
        {
            _connection = new SQLiteConnection();
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO networkmetrics(idagent, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<NetworkMetricsModel> GetAll()
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM networkmetrics";
            var returnList = new List<NetworkMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    });
                }
            }
            _connection.Dispose();

            return returnList;
        }

        public NetworkMetricsModel GetById(int id)
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    NetworkMetricsModel model = new NetworkMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    };
                    _connection.Dispose();

                    return model;
                }
                else
                {
                    _connection.Dispose();
                    return null;
                }
            }
        }

        public List<NetworkMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE time > {from} AND time < {to} AND agentId = {id}";
            var returnList = new List<NetworkMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new NetworkMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3))
                    });
                }
            }

            _connection.Dispose();
            return returnList;
        }
    }
}
