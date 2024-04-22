using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Web.DTOs;
using Shared.Client;
using WebShared.Identity.Team.AdminManagement;

namespace Web.Client.Components.ManageTeam.Members.Modals
{
    public partial class RevokeInvitationCompBase : BaseComponent
    {
        [Parameter] public MemberDTO Member { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        [Parameter] public TeamAdminInfoDTO TeamAdminInfo { get; set; }
        public async Task RevokeInvitation()
        {
            await httpClientService.PostToAPIAsync("/teamAdmin/invite/revoke", new RevokeInvitationDTO { TeamId = TeamAdminInfo.Id, UserId = Member.Id });
            await CloseModal();
        }
        public async Task CloseModal()
        {
            await ModalInstance.CloseAsync();
        }
    }
}
