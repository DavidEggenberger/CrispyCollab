using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Security.Claims;
using WebWasmClient.Services;

namespace WebWasmClient.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [Inject] public HttpClientService HttpClientService { get; set; }
        [Inject] public ValidationService ValidationService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public HubConnection HubConnection { get; set; }
        [CascadingParameter] public string TenantName { get; set; }
    }
}
