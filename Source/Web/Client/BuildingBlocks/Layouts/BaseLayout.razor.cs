using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Web.Client.BuildingBlocks.Layouts
{
    public partial class BaseLayout : LayoutComponentBase
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
