using Blazored.Modal;
using WebShared.Identity.DTOs.TeamDTOs;
using WebShared.Identity.Team;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
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
