using Microsoft.JSInterop;

namespace Web.Client.BuildingBlocks.Authentication.Antiforgery
{
    public class AntiforgeryTokenService
    {
        private readonly IJSRuntime jSRuntime;
        public AntiforgeryTokenService(IJSRuntime jsRuntime)
        {
            jSRuntime = jsRuntime;
        }
        public async Task<string> GetAntiforgeryTokenAsync()
        {
            return await jSRuntime.InvokeAsync<string>("getAntiForgeryToken");
        }
    }
}
