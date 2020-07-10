using System;
using CityManager.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;

namespace CityManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<CityManagerDbContext>();
                    dbContext.Database.Migrate();
                    dbContext.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
            .ConfigureServices(serviceCollection => serviceCollection
               .Configure<AzureFileLoggerOptions>(options =>
               {
                   options.FileName = "azure-diagnostics-";
                   options.FileSizeLimit = 50 * 1024;
                   options.RetainedFileCountLimit = 5;
               })
               .Configure<AzureBlobLoggerOptions>(options =>
               {
                   options.BlobName = "log.txt";
               }))
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                IHostEnvironment hostingEnvironment = hostingContext.HostingEnvironment;
                IConfigurationBuilder configurationBuilder = config;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            });
    }
}