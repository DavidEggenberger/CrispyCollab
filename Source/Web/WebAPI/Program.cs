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

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
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
