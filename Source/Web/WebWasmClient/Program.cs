using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using WebWasmClient.Authentication;
using WebWasmClient.Authentication.Antiforgery;
using Blazored.Modal;
using WebWasmClient.Services;
using WebShared.Authorization;

namespace WebWasmClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            #region HttpClients
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<HttpClientService>();
            builder.Services.AddHttpClient("default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("default"));
            builder.Services.AddTransient<AuthorizedHandler>();
            builder.Services.AddHttpClient("authorizedClient", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<AuthorizedHandler>();
            #endregion
            #region Authentication
            builder.Services.AddSingleton<AntiforgeryTokenService>();
            builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
            builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
            builder.Services.RegisterAuthorization();
            #endregion
            builder.Services.AddBlazoredModal();
            builder.Services.AddValidation(typeof(WebShared.IAssemblyMarker).Assembly);

            await builder.Build().RunAsync();
        }
    }
}
