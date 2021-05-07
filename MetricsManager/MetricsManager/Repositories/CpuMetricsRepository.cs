using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetricsModel>
    {
        List<CpuMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private SQLiteConnection _connection;

        public void Create(CpuMetricsModel model)
        {
            _connection = new SQLiteConnection();
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO cpumetrics(agentId, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<CpuMetricsModel> GetAll()
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM cpumetrics";
            var returnList = new List<CpuMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetricsModel
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

        public CpuMetricsModel GetById(int id)
        {
            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM cpumetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    CpuMetricsModel model = new CpuMetricsModel
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

        public List<CpuMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM cpumetrics WHERE time > {from} AND time < {to} AND agentId = {id}";
            var returnList = new List<CpuMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new CpuMetricsModel
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
