using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Web.DTOs;

namespace WebWasmClient.Components.Dashboard.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        public HttpClientService httpClientService { get; set; }
        public List<TeamDTO> Teams { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await httpClientService.GetFromAPIAsync<List<TeamDTO>>("api/Team/all");
        }    
    }
}
