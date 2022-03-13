using Blazored.Modal;
using Common.Identity.Team;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebWasmClient.Features.ManageTeam.Members.Modals;

namespace WebWasmClient.Features.ManageTeam.Members
{
    public partial class ManageMembersComp
    {
        [Parameter] public List<MemberDTO> Members { get; set; }

        void ShowInvitationRevokeModal(MemberDTO member)
        {
            var parameters = new ModalParameters();
            parameters.Add("Member", member);
            Modal.Show<RevokeInvitationComp>(string.Empty, parameters);
        }
    }
}
