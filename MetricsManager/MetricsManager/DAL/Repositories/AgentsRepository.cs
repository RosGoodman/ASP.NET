﻿using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    public interface IAgentRepository : IRepository<AgentModel>
    {
        AgentModel GetById(long id);
    }

    public class AgentsRepository : IAgentRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public AgentsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        /// <summary>Получить весь список агентов.</summary>
        /// <returns>Список агентов.</returns>
        public IList<AgentModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<AgentModel>("SELECT * FROM agents").ToList();
        }

        /// <summary>Получить агента по id.</summary>
        /// <param name="id">id агента.</param>
        /// <returns>Искомый агент или null.</returns>
        public AgentModel GetById(long id)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<AgentModel>("SELECT * FROM agents WHERE id = @id",
                new
                {
                    id = id
                });
            }
        }

        /// <summary>Добавить агента.</summary>
        /// <param name="item">Модель добавляемого агента.</param>
        public void Create(AgentModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO agents(name, address) VALUES(@name, @address)",
                    new
                    {
                        name = model.Name,
                        address = model.Address
                    });
            }
        }

        public DateTimeOffset GetLastTime()
        {
            throw new NotImplementedException();
        }

        public AgentModel GetByRecordNumb(long id, long numb)
        {
            throw new NotImplementedException();
        }
    }
}
