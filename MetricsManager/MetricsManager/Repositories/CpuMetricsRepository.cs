﻿using MetricsManager.Models;
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
        private static SQLiteConnection _connection;

        public void Create(CpuMetricsModel model)
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO cpumetrics(agentId, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public IList<CpuMetricsModel> GetAll()
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

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

            return returnList;
        }

        public CpuMetricsModel GetById(int id)
        {
            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM cpumetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new CpuMetricsModel
                    {
                        AgentId = reader.GetInt32(1),
                        Value = reader.GetInt32(2),
                        DateTime = DateTimeOffset.FromUnixTimeSeconds(reader.GetInt32(3))
                    };
                }
                else return null;
            }
        }

        public List<CpuMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            using var _connection = new SQLiteConnection("Data Source=Metrics.db");
            _connection.Open();

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

            return returnList;
        }
    }
}
