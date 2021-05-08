using MetricsManager.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();
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
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, AgentId INT, value INT, time INT,
                    FOREIGN KEY(AgentId) REFERENCES agents(id))";
                command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY, Id INT, value INT, time INT,
                    FOREIGN KEY(Id) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, AgentId INT, value INT, time INT,
                    FOREIGN KEY(AgentId) REFERENCES agents(id))";
                command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, Id INT, value INT, time INT,
                    FOREIGN KEY(Id) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, AgentId INT, value INT, time INT,
                    FOREIGN KEY(AgentId) REFERENCES agents(id))";
                command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, Id INT, value INT, time INT,
                    FOREIGN KEY(Id) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, AgentId INT, value INT, time INT,
                    FOREIGN KEY(AgentId) REFERENCES agents(id))";
                command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, Id INT, value INT, time INT,
                    FOREIGN KEY(Id) REFERENCES agents(id))";
                command.ExecuteNonQuery();

                command.CommandText = "DROP TABLE IF EXISTS rammetrics";
                command.ExecuteNonQuery();
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, AgentId INT, value INT, time INT,
                    FOREIGN KEY(AgentId) REFERENCES agents(id))";
                command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, Id INT, value INT, time INT,
                    FOREIGN KEY(Id) REFERENCES agents(id))";
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

                command.CommandText = "INSERT INTO cpumetrics(AgentId, value, time) VALUES(1, 15, 1577998800)";     //1577998800 = 02.01.2020 21:00
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(AgentId, value, time) VALUES(2, 100, 1578344400)";    //1578344400 = 06.01.2020 21:00
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO dotnetmetrics(AgentId, value, time) VALUES(1, 16, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(AgentId, value, time) VALUES(2, 99, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO hddmetrics(AgentId, value, time) VALUES(1, 323232, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(AgentId, value, time) VALUES(2, 121221, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO networkmetrics(AgentId, value, time) VALUES(1, 101, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(AgentId, value, time) VALUES(2, 131, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO rammetrics(AgentId, value, time) VALUES(1, 86543, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(AgentId, value, time) VALUES(2, 9652165, 1578344400)";

                command.CommandText = "INSERT INTO cpumetrics(Id, value, time) VALUES(1, 15, 1577998800)";     //1577998800 = 02.01.2020 21:00
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO cpumetrics(Id, value, time) VALUES(2, 100, 1578344400)";    //1578344400 = 06.01.2020 21:00
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO dotnetmetrics(Id, value, time) VALUES(1, 16, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO dotnetmetrics(Id, value, time) VALUES(2, 99, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO hddmetrics(Id, value, time) VALUES(1, 323232, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO hddmetrics(Id, value, time) VALUES(2, 121221, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO networkmetrics(Id, value, time) VALUES(1, 101, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO networkmetrics(Id, value, time) VALUES(2, 131, 1578344400)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO rammetrics(Id, value, time) VALUES(1, 86543, 1577998800)";
                command.ExecuteNonQuery();
                command.CommandText = "INSERT INTO rammetrics(Id, value, time) VALUES(2, 9652165, 1578344400)";

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
