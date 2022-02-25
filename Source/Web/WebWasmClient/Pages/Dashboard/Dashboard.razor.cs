using Common.DTOs.Identity.Tenant;
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
        public List<TenantDTO> Tenants { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Tenants = await HttpClientFactory.CreateClient("authorizedClient").GetFromJsonAsync<List<TenantDTO>>("api/tenant/all");
        }
        
    }
}
