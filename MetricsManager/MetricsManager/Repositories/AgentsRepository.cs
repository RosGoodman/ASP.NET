using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager.Repositories
{
    public interface IAgentRepository : IRepository<AgentModel>
    {

    }

    public class AgentsRepository : IAgentRepository
    {
        private SQLiteConnection _connection;

        public AgentsRepository(SQLiteConnection connection)
        {
            _connection = connection;
        }

        /// <summary>Получить весь список агентов.</summary>
        /// <returns>Список агентов.</returns>
        public IList<AgentModel> GetAll()
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = "SELECT * FROM agents";
            var returnList = new List<AgentModel>();
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    returnList.Add(new AgentModel
                    {
                        AgentId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }
            }

            return returnList;
        }

        /// <summary>Получить агента по id.</summary>
        /// <param name="id">id агента.</param>
        /// <returns>Искомый агент или null.</returns>
        public AgentModel GetById(int id)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"SELECT * FROM agents WHERE id = {id}";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new AgentModel
                    {
                        AgentId = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                }
                else return null;
            }
        }

        /// <summary>Добавить агента.</summary>
        /// <param name="item">Модель добавляемого агента.</param>
        public void Create(AgentModel item)
        {
            using var cmd = new SQLiteCommand(_connection);
            cmd.CommandText = $"INSERT INTO agents(name) VALUES('{item.Name}')";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
