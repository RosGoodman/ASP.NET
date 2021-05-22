using FluentMigrator.Runner;
using MetricsManager.Client;
using MetricsManager.Jobs;
using MetricsManager.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Data.SQLite;
using Polly;
using System;

namespace MetricsManager
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));

            services.AddControllers();
            ConfigureServiceConnection(services);
            services.AddSingleton<IAgentRepository, AgentsRepository>();
            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();

            // ДОбавляем сервисы
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // добавляем нашу задачу
            services.AddSingleton<CpuMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricsFromAgents),
                cronExpression: "0/20 * * * * ?")); // запускать каждые 30 секунд

            services.AddSingleton<HddMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricsFromAgents),
                cronExpression: "0/20 * * * * ?"));

            services.AddSingleton<DotNetMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricsFromAgents),
                cronExpression: "0/20 * * * * ?"));

            services.AddSingleton<NetworkMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricsFromAgents),
                cronExpression: "0/20 * * * * ?"));

            services.AddSingleton<RamMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricsFromAgents),
                cronExpression: "0/20 * * * * ?"));

            services.AddHostedService<QuartzHostedServise>();
        }

        private void ConfigureServiceConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=Metrics.db";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();

            services.AddFluentMigratorCore().ConfigureRunner(builder => builder
                //добавляем поддержку SQLite
                .AddSQLite()
                //устанавливаем сроку подключения
                .WithGlobalConnectionString(connectionString)
                //подсказываем где искать классы с миграциями
                .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
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

            migrationRunner.MigrateUp(1);
        }
    }
}
