﻿using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    public interface IDotNetMetricsRepository : IRepository<DotNetMetricsModel>
    {
        List<DotNetMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class DotNetMetricsRepository : IDotNetMetricsRepository
    {
        private static SQLiteConnection _connection;

        public void Create(DotNetMetricsModel model)
        {
            DBConnectionOpen();

            using SQLiteCommand cmd = new(_connection);
            cmd.CommandText = $"INSERT INTO dotnetmetrics(idagent, value, time) VALUES({model.AgentId}, {model.Value}, {model.DateTime.ToUnixTimeSeconds()})";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            _connection.Dispose();
        }

        public IList<DotNetMetricsModel> GetAll()
        {
            DBConnectionOpen();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM dotnetmetrics";
            var returnList = new List<DotNetMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricsModel
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

        public DotNetMetricsModel GetById(int id)
        {
            DBConnectionOpen();

            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE agentId = {id}";

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    DotNetMetricsModel model = new DotNetMetricsModel
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

        public List<DotNetMetricsModel> GetMetricsFromeTimeToTimeFromAgent(int id, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            long from = fromTime.ToUnixTimeSeconds();
            long to = toTime.ToUnixTimeSeconds();

            _connection = new SQLiteConnection();
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM dotnetmetrics WHERE time > {from} AND time < {to} AND agentId = {id}";
            var returnList = new List<DotNetMetricsModel>();

            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new DotNetMetricsModel
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

        private static void DBConnectionOpen()
        {
            const string connectionString = "Data Source=Metrics.db";
            _connection = new SQLiteConnection(connectionString);
            _connection.Open();
        }
    }
}
