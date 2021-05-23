using Dapper;
using MetricsManager.DAL;
using MetricsManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace MetricsManager.Repositories
{
    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetricsModel>
    {
        List<CpuMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long id, DateTimeOffset fromTime, DateTimeOffset toTime);
    }

    public class CpuMetricsRepository : ICpuMetricsRepository
    {
        private const string ConnectionString = "Data Source=Metrics.db";

        public CpuMetricsRepository()
        {
            SqlMapper.AddTypeHandler(new DateTimeOffsetHandler());
        }

        public void Create(CpuMetricsModel model)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute("INSERT INTO cpumetrics(agentid, value, time) VALUES(@agentid, @value, @time)",
                    new
                    {
                        agentid = model.AgentId,
                        value = model.Value,
                        time = model.Time
                    });
            }
        }

        public IList<CpuMetricsModel> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<CpuMetricsModel>("SELECT * FROM cpumetrics").ToList();
        }

        public CpuMetricsModel GetByRecordNumb(long id, long recordNumb)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                return connection.QueryFirstOrDefault<CpuMetricsModel>("SELECT * From cpumetrics WHERE agentid = @agentid AND id = @id", new
                {
                    AgentId = id,
                    id = recordNumb
                });
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////
        //В целом все работает, но что-то напутал с запросами, пока не разобрался где проблема
        ///////////////////////////////////////////////////////////////////////////////////////

        public List<CpuMetricsModel> GetMetricsFromeTimeToTimeFromAgent(long agentid, DateTimeOffset fromTime, DateTimeOffset toTime)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            return connection.Query<CpuMetricsModel>($"SELECT * From cpumetrics WHERE time >= @fromTime AND time <= @toTime AND AgentId = @agentid",
                    new
                    {
                        agentid = agentid,
                        fromTime = fromTime.ToUnixTimeSeconds(),
                        toTime = toTime.ToUnixTimeSeconds()
                    }).ToList();
        }

        public DateTimeOffset GetLastTime()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.QueryFirstOrDefault<CpuMetricsModel>("SELECT * FROM cpumetrics ORDER BY time DESC LIMIT 1");
            if (result == null)
            {
                result = new CpuMetricsModel();
                result.Time = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            }
            return result.Time;
        }
    }
}
