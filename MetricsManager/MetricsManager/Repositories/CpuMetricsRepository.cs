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

    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        //private SQLiteConnection _connection;

        //public CpuMetricsRepository(SQLiteConnection connection)
        //{
        //    _connection = connection;
        //}

        public void Create(CpuMetricsModel model)
        {
            //using SQLiteCommand cmd = new SQLiteCommand(_connection);
            //cmd.CommandText = $"INSERT INTO cpumetrics(idagent, value, time) VALUES({model.Value}, {model.DateTime.ToUnixTimeSeconds()}, {model.Id})";
            //cmd.Prepare();
            //cmd.ExecuteNonQuery();
        }

        public IList<CpuMetricsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public CpuMetricsModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(CpuMetricsModel item)
        {
            throw new NotImplementedException();
        }
        public List<CpuMetricsModel> GetMetricsFromeTimeToTime(DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            return null;
        }
    }
}
