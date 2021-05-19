using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Diagnostics;

namespace MetricsAgent
{
    public class Program
    {
        public static void Main(string[] args)
        {

            PerformanceCounterCategory[] categories = PerformanceCounterCategory.GetCategories();
            foreach (var category in categories)
            {
                string[] instanceNames = category.GetInstanceNames();
                foreach (string instanceName in instanceNames)
                    Console.WriteLine(instanceName.ToString());
                    //PerformanceCounter[] counters = category.GetCounters(instanceName);
            }

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            // отлов всех исключений в рамках работы приложения
            catch (Exception exception)
            {
                //NLog: устанавливаем отлов исключений
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // остановка логера 
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureLogging(logging =>
                {
                    logging.ClearProviders(); // создание провайдеров логирования
                    logging.SetMinimumLevel(LogLevel.Trace); // устанавливаем минимальный уровень логирования
                })
            .UseNLog(); // добавляем библиотеку nlog

    }
}
