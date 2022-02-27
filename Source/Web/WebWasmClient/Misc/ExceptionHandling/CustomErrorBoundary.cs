using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Threading.Tasks;

namespace WebWasmClient.Framework.ExceptionHandling
{
    public class CustomErrorBoundary : ErrorBoundary
    {
        [Inject]
        private IWebAssemblyHostEnvironment env { get; set; }
        protected override Task OnErrorAsync(Exception exception)
        {
            if (env.IsDevelopment())
            {
                return base.OnErrorAsync(exception);
            }
            return Task.CompletedTask;
        }
    }
}
