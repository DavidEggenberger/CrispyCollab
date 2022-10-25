using Modules.IdentityModule.Web.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWasmClient.Services;

namespace WebWasmClient.Layouts.MainLayoutMenu
{
    public partial class TeamsOverviewComp
    {
        private IEnumerable<TeamDTO> teams;
        protected override async Task OnInitializedAsync()
        {
            teams = await HttpClientService.GetFromAPIAsync<IEnumerable<TeamDTO>>("/user/allTeams");
        }
        private bool expanded = true;
        public void Click()
        {
            expanded = !expanded;
            StateHasChanged();
        }
    }
}
