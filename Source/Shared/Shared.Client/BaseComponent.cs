using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shared.Web.Client.Services;
using System.Security.Claims;

namespace Shared.Web.Client
{
    public class BaseComponent : ComponentBase
    {
        [Inject] public HttpClientService HttpClientService { get; set; }
        [Inject] public ValidationService ValidationService { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public string TenantName { get; set; }
    }
}
