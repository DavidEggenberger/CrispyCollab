using Blazored.Modal.Services;
using Common.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Security.Claims;
using WebWasmClient.Services;

namespace WebWasmClient.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [CascadingParameter] public ValidationService ValidationService { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public HubConnection HubConnection { get; set; }
    }
}
