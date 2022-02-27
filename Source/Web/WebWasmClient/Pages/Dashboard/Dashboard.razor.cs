using Common.DTOs.Identity.Team;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebWasmClient.Features.Dashboard.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject]
        public IHttpClientFactory HttpClientFactory { get; set; }
        public List<TeamDTO> Teams { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Teams = await HttpClientFactory.CreateClient("authorizedClient").GetFromJsonAsync<List<TeamDTO>>("api/Team/all");
        }
        
    }
}
