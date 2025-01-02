using Modules.TenantIdentity.Public.DTOs.Tenant;
using Shared.Client;

namespace Web.Client.BuildingBlocks.Layouts.MainLayoutMenu
{
    public partial class TeamsOverviewComponentBase : BaseComponent
    {
        protected IEnumerable<TenantDTO> teams;
        protected override async Task OnInitializedAsync()
        {
            teams = await httpClientService.GetFromAPIAsync<IEnumerable<TenantDTO>>("/user/allTeams");
        }
        protected bool expanded = true;
        public void Click()
        {
            expanded = !expanded;
            StateHasChanged();
        }
    }
}
