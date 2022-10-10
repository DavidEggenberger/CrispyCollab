using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using WebWasmClient.Components.ManageTeam.Members.Modals;

namespace WebWasmClient.Components.ManageTeam.Members
{
    public partial class ManageMembersComp
    {
        [Parameter] public List<MemberDTO> Members { get; set; }
        [CascadingParameter] public TeamAdminInfoDTO TeamAdminInfo { get; set; }
        void ShowInvitationRevokeModal(MemberDTO member)
        {
            var parameters = new ModalParameters();
            parameters.Add("Member", member);
            parameters.Add("TeamAdminInfo", TeamAdminInfo);
            Modal.Show<RevokeInvitationComp>(string.Empty, parameters);
        }
    }
}
