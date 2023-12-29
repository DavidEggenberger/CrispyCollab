using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace WebWasmClient.Layouts
{
    public partial class BaseLayout
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("hideLoadingScreen");
            }
        }
    }
}
