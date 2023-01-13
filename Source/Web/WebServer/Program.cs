using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EFCore;
using Shared.Infrastructure.EFCore.Migrations;

namespace WebServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                using IHost host = CreateHostBuilder(args).Build();
                using IServiceScope serviceScope = host.Services.CreateScope();

                host.Run();
            }
            catch (Exception ex)
            {
                #region Logging
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
                #endregion
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
                    configuration.MinimumLevel.Information();
                })
                .ConfigureAppConfiguration((hostBuilderContext, configuration) =>
                {
                    //configuration.AddAzureKeyVault(new Uri(""),
                    //        new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "b803e77c-0003-4a3a-8d33-861eb2e3ebbf" }));
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
