using MetricsAgent.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SQLite;

namespace MetricsAgent
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            ConfigureServiceConnection(services);
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
            connection.Dispose();
        }

        private static void PrepareSchema(SQLiteConnection connection)
        {
            using var command = new SQLiteCommand(connection);
            command.CommandText = "DROP TABLE IF EXISTS cpumetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE cpumetrics(id INTEGER PRIMARY KEY,value INT, time INT)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS dotnetmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE dotnetmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS hddmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE hddmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS networkmetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE networkmetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
            command.ExecuteNonQuery();

            command.CommandText = "DROP TABLE IF EXISTS rammetrics";
            command.ExecuteNonQuery();
            command.CommandText = @"CREATE TABLE rammetrics(id INTEGER PRIMARY KEY, value INT, time INT)";
            command.ExecuteNonQuery();

            CreateData(connection); //данные для проверки
        }

        #region CreateData

        //TODO: удалить после завершения проверок

        /// <summary>Временный метод для внесения данных в БД</summary>
        /// <param name="conection"></param>
        private static void CreateData(SQLiteConnection connection)
        {
            using var command = new SQLiteCommand(connection);
            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(15, 1577998800)";     //1577998800 = 02.01.2020 21:00
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO cpumetrics(value, time) VALUES(15, 1577998800)";     //1577998800 = 02.01.2020 21:00
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(16, 1577998800)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO dotnetmetrics(value, time) VALUES(99, 1578344400)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(323232, 1577998800)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO hddmetrics(value, time) VALUES(121221, 1578344400)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(101, 1577998800)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO networkmetrics(value, time) VALUES(131, 1578344400)";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(86543, 1577998800)";
            command.ExecuteNonQuery();
            command.CommandText = "INSERT INTO rammetrics(value, time) VALUES(9652165, 1578344400)";
            command.ExecuteNonQuery();
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
