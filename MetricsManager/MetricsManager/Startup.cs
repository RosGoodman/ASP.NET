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
using AutoMapper;
using MetricsManager.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace MetricsManager
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region swagger
            services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API ������� ������ ����� ������",
                    Description = "��� ����� �������� � api ������ �������",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Pavel",
                        Email = string.Empty,
                        Url = new Uri("https://kremlin.ru"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "����� ������� ��� ����� ��������� ��� ������������",
                        Url = new Uri("https://example.com/license"),
                    }
                });
                // ��������� ���� �� �������� ����� ����������� ��� Swagger UI
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

            var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfiles()));
            var mapper = mapperConfiguration.CreateMapper();
            services.AddSingleton(mapper);

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

            #region jobs
            // ��������� �������
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // ��������� ���� ������
            services.AddSingleton<CpuMetricsFromAgents>();
            services.AddSingleton(new JobSchedule(
                jobType: typeof(CpuMetricsFromAgents),
                cronExpression: "0/20 * * * * ?")); // ��������� ������ 30 ������

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
            #endregion
        }

        private void ConfigureServiceConnection(IServiceCollection services)
        {
            const string connectionString = "Data Source=Metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
            var connection = new SQLiteConnection(connectionString);
            connection.Open();

            services.AddFluentMigratorCore().ConfigureRunner(builder => builder
                //��������� ��������� SQLite
                .AddSQLite()
                //������������� ����� �����������
                .WithGlobalConnectionString(connectionString)
                //������������ ��� ������ ������ � ����������
                .ScanIn(typeof(Startup).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            // ��������� middleware � �������� ��� ��������� Swagger ��������.
            app.UseSwagger();
            // ��������� middleware ��� ��������� swagger-ui 
            // ��������� Swagger JSON �������� (���� ���������� �� ��������������� �������������
            // �� ������� ����� �������� UI).
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API ������� ��������� ����� ������");
            });

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
