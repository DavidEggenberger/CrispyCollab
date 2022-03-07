using Blazored.Modal.Services;
using Common.DTOs.Identity.Team;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;

namespace WebWasmClient.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public TeamDTO Team { get; set; }
        [CascadingParameter] public HubConnection HubConnection { get; set; }
    }
}
