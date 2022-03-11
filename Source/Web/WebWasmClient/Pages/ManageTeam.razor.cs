using Common.Identity.DTOs.TeamDTOs;
using System.Threading.Tasks;

namespace WebWasmClient.Pages
{
    public partial class ManageTeam
    {
        private bool loading = true;
        private TeamAdminInfoDTO teamAdminInfo;
        protected override async Task OnInitializedAsync()
        {
            teamAdminInfo = await HttpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/teamAdmin");
            loading = false;
        }
    }
}
