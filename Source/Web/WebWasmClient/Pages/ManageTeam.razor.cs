using WebShared.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace WebWasmClient.Pages
{
    public partial class ManageTeam
    {
        private bool loading = true;
        private TeamAdminInfoDTO teamAdminInfo;
        protected override async Task OnInitializedAsync()
        {
            teamAdminInfo = await HttpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
            loading = false;

            HubConnection.On("UpdateAdminInformation", async () =>
            {
                teamAdminInfo = await HttpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
                StateHasChanged();
            });
        }
    }
}
