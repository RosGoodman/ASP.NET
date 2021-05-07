using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    public interface IRamMetricsRepository : IRepository<RamMetricsModel>
    {
        List<RamMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class RamMetricsRepository : IRamMetricsRepository
    {
        private SQLiteConnection _connection;

        public void Create(RamMetricsModel model)
        {
            _connection = new SQLiteConnection();
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO rammetrics(idagent, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<RamMetricsModel> GetAll()
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM rammetrics";
            var returnList = new List<RamMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetricsModel
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

        public RamMetricsModel GetById(int id)
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM rammetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    RamMetricsModel model = new RamMetricsModel
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

        public List<RamMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM rammetrics WHERE time > {from} AND time < {to} AND agentId = {id}";
            var returnList = new List<RamMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new RamMetricsModel
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
