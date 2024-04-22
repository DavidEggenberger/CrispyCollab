using Modules.IdentityModule.Web.DTOs;
using Shared.Client;

namespace Web.Web.Client.Pages
{
    public partial class ManageTeamBase : BaseComponent
    {
        protected bool loading = true;
        protected TeamAdminInfoDTO teamAdminInfo;
        protected override async Task OnInitializedAsync()
        {
            teamAdminInfo = await httpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
            loading = false;

            //HubConnection.On("UpdateAdminInformation", async () =>
            //{
            //    teamAdminInfo = await HttpWeb.ClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
            //    StateHasChanged();
            //});
        }
    }
}
