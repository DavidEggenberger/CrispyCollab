using Microsoft.AspNetCore.SignalR.Client;
using Modules.IdentityModule.Web.DTOs;
using Shared.Web.Client;
using Shared.Web.Client.Services;
using System.Threading.Tasks;

namespace Web.Client.Pages
{ 
    public partial class ManageTeamBase : BaseComponent
    {
        protected bool loading = true;
        protected TeamAdminInfoDTO teamAdminInfo;
        protected override async Task OnInitializedAsync()
        {
            teamAdminInfo = await HttpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
            loading = false;

            //HubConnection.On("UpdateAdminInformation", async () =>
            //{
            //    teamAdminInfo = await HttpClientService.GetFromAPIAsync<TeamAdminInfoDTO>("/team");
            //    StateHasChanged();
            //});
        }
    }
}
