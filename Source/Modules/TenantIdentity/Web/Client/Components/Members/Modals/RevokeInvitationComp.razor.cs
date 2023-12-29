using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Web.DTOs;
using WebShared.Identity.Team.AdminManagement;

namespace WebWasmClient.Components.ManageTeam.Members.Modals
{
    public partial class RevokeInvitationCompBase : BaseComponent
    {
        [Parameter] public MemberDTO Member { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        [Parameter] public TeamAdminInfoDTO TeamAdminInfo { get; set; }
        public async Task RevokeInvitation()
        {
            await HttpClientService.PostToAPIAsync("/teamAdmin/invite/revoke", new RevokeInvitationDTO { TeamId = TeamAdminInfo.Id, UserId = Member.Id });
            await CloseModal();
        }
        public async Task CloseModal()
        {
            await ModalInstance.CloseAsync();
        }
    }
}
