using Modules.IdentityModule.Shared;
using Shared.Client;

namespace Web.Client.Layouts.MainLayoutMenu
{
    public partial class TeamsOverviewComponentBase : BaseComponent
    {
        protected IEnumerable<TeamDTO> teams;
        protected override async Task OnInitializedAsync()
        {
            teams = await httpClientService.GetFromAPIAsync<IEnumerable<TeamDTO>>("/user/allTeams");
        }
        protected bool expanded = true;
        public void Click()
        {
            expanded = !expanded;
            StateHasChanged();
        }
    }
}
