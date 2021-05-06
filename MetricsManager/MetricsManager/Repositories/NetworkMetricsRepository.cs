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

        public NetworkMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(NetworkMetricsModel model)
        {
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO networkmetrics(idagent, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<NetworkMetricsModel> GetAll()
        {
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

            return returnList;
        }

        public NetworkMetricsModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM networkmetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new NetworkMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    };
                }
                else return null;
            }
        }

        public List<NetworkMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

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
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    });
                }
            }

            return returnList;
        }
    }
}
