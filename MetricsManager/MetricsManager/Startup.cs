using MetricsManager.Models;
using MetricsManager.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Data.SQLite;

namespace MetricsManager
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureServiceConnection(services);
            services.AddSingleton<IAgentRepository, AgentsRepository>();
            services.AddSingleton<CpuMetricsRepository>();
            services.AddSingleton<DotNetMetricsRepository>();
            services.AddSingleton<HddMetricsRepository>();
            services.AddSingleton<NetworkMetricsRepository>();
            services.AddSingleton<RamMetricsRepository>();
        }

        private void ConfigureServiceConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=Metrics.db";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            PrepareSchema(connection);
            services.AddSingleton(connection);
        }

        private void PrepareSchema(SQLiteConnection connection)
        {
            using(var command = new SQLiteCommand(connection))
            {
                // задаем новый текст команды для выполнения
                // удаляем таблицу с метриками если она существует в базе данных
                command.CommandText = "DROP TABLE IF EXISTS agents";
                // отправляем запрос в базу данных
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE agents(id INTEGER PRIMARY KEY, name STRING)";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, agentId INT, value INT, time INT,
                    FOREIGN KEY(agentId) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, agentId INT, value INT, time INT,
                    FOREIGN KEY(agentId) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, agentId INT, value DOUBLE, time INT,
                    FOREIGN KEY(agentId) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, agentid INT, value INT, time INT,
                    FOREIGN KEY(agentId) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, agentId INT, value DOUBLE, time INT,
                    FOREIGN KEY(agentId) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                CreateData(connection); //данные для проверки
            }
        }

        #region CreateData

        //TODO: удалить после завершения проверок

        /// <summary>Временный метод для внесения данных в БД</summary>
        /// <param name="conection"></param>
        private void CreateData(SQLiteConnection connection)
        {
            using(var command = new SQLiteCommand(connection))
            {
                command.CommandText = "INSERT INTO agents(name) VALUES('agent1')";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO agents(name) VALUES('agent2')";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO cpumetrics(agentId, value, time) VALUES(1, 15, 5)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(agentId, value, time) VALUES(2, 100, 20)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO dotnetmetrics(agentId, value, time) VALUES(1, 16, 6)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(agentId, value, time) VALUES(2, 99, 11)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO hddmetrics(agentId, value, time) VALUES(1, 1345.123, 7)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(agentId, value, time) VALUES(2, 12345.1234, 12)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO networkmetrics(agentId, value, time) VALUES(1, 101, 8)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(agentId, value, time) VALUES(2, 131, 13)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO rammetrics(agentId, value, time) VALUES(1, 1234.123, 9)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(agentId, value, time) VALUES(2, 222.22, 14)";
                command.ExecuteNonQuery();
            }
        }

        #endregion

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
