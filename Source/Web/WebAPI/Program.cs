using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Identity;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using IHost host = CreateHostBuilder(args).Build();
                using IServiceScope serviceScope = host.Services.CreateScope();
                IdentificationDbContext identificationDbContext = serviceScope.ServiceProvider.GetRequiredService<IdentificationDbContext>();                
                serviceScope.ServiceProvider.GetRequiredService<IdentificationDbContext>().Users.Count();
                host.Run();
            }
            catch (Exception ex)
            {
                // Log.Logger will likely be internal type "Serilog.Core.Pipeline.SilentLogger".
                // Loading configuration or Serilog failed.
                // This will create a logger that can be captured by Azure logger.
                // To enable Azure logger, in Azure Portal:
                // 1. Go to WebApp
                // 2. App Service logs
                // 3. Enable "Application Logging (Filesystem)", "Application Logging (Filesystem)" and "Detailed error messages"
                // 4. Set Retention Period (Days) to 10 or similar value
                // 5. Save settings
                // 6. Under Overview, restart web app
                // 7. Go to Log Stream and observe the logs
                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {
                    Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console()
                        .CreateLogger();
                }
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(options =>
                {
                    options.ClearProviders();
                })
                .UseSerilog((hostBuilderContext, configuration) =>
                {
                    configuration.WriteTo.Console();
                })
                .ConfigureAppConfiguration((hostBuilderContext, configuration) =>
                {
                    //configuration.AddAzureKeyVault(new Uri("https://hacksgkeyvault.vault.azure.net/"),
                    //        new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "b803e77c-0003-4a3a-8d33-861eb2e3ebbf" }));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
