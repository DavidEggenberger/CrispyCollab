using Microsoft.JSInterop;
using System.Threading.Tasks;
using WebWasmClient.Framework.ExceptionHandling;

namespace WebWasmClient.Layout
{
    public partial class MainLayout
    {
        private CustomErrorBoundary errorBoundary;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("hideLoadingScreen");
            }
        }
    }
}
