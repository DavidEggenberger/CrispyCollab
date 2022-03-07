using Common.DTOs.Identity.Team;
using Common.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebWasmClient.Services;

namespace WebWasmClient.Features.Dashboard.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        public HttpClientService httpClientService { get; set; }
        public List<TeamDTO> Teams { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await httpClientService.GetFromAPI<List<TeamDTO>>("api/Team/all");
        }    
    }
}
