using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebWasmClient.Services;
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
