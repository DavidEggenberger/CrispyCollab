using Common.Identity.DTOs.TeamDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Layout.Menu
{
    public partial class TeamsOverviewComp
    {
        private List<TeamDTO> teams;
        protected override async Task OnInitializedAsync()
        {
            teams = await HttpClientService.GetFromAPIAsync<List<TeamDTO>>("/user/allTeams");
        }
        private bool expanded = true;
        public void Click()
        {
            expanded = !expanded;
            StateHasChanged();
        }
    }
}
