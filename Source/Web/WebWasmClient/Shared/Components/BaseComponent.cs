using Blazored.Modal.Services;
using Common.DTOs.Identity.Team;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace WebWasmClient.Shared.Components
{
    public class BaseComponent : ComponentBase
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [CascadingParameter] public ClaimsPrincipal User { get; set; }
        [CascadingParameter] public TeamAuthentiicationDTO Team { get; set; }
    }
}
