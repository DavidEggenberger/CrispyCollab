using Modules.IdentityModule.Web.DTOs;
using Shared.Web.Client;

namespace WebWasmClient.Layouts.MainLayoutMenu
{
    public partial class TeamsOverviewComponentBase : BaseComponent
    {
        protected IEnumerable<TeamDTO> teams;
        protected override async Task OnInitializedAsync()
        {
            teams = await HttpClientService.GetFromAPIAsync<IEnumerable<TeamDTO>>("/user/allTeams");
        }
        protected bool expanded = true;
        public void Click()
        {
            expanded = !expanded;
            StateHasChanged();
        }
    }
}
