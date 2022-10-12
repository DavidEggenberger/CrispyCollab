using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebShared.Identity.Team.AdminManagement;

namespace WebWasmClient.Components.ManageTeam.Members.Modals
{
    public partial class RevokeInvitationComp
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
