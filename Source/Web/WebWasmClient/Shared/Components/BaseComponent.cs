using Blazored.Modal.Services;
using Common.DTOs.Identity.Team;
using Common.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;

namespace WebWasmClient.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public TeamAuthenticationDTO Team { get; set; }
        [CascadingParameter] public HubConnection HubConnection { get; set; }
    }
}
