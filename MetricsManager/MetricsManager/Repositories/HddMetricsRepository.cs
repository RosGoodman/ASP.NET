﻿using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    public interface IHddMetricsRepository : IRepository<HddMetricsModel>
    {
        List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class HddMetricsRepository : IHddMetricsRepository
    {
        private SQLiteConnection _connection;

        public HddMetricsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void Create(HddMetricsModel model)
        {
            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO cpumetrics(idagent, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<HddMetricsModel> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM hddmetrics";
            var returnList = new List<HddMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    });
                }
            }

            return returnList;
        }

        public HddMetricsModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new HddMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    };
                }
                else return null;
            }
        }

        public List<HddMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM hddmetrics WHERE time > {from} AND time < {to} AND agentId = {id}";
            var returnList = new List<HddMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new HddMetricsModel
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
