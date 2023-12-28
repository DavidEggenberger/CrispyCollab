using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Web.DTOs;
using Shared.Web.Client;
using WebWasmClient.Components.ManageTeam.Members.Modals;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class ManageMembersComponentBase : BaseComponent
    {
        [Parameter] public List<MemberDTO> Members { get; set; }
        [CascadingParameter] public TeamAdminInfoDTO TeamAdminInfo { get; set; }
        void ShowInvitationRevokeModal(MemberDTO member)
        {
            var parameters = new ModalParameters();
            parameters.Add("Member", member);
            parameters.Add("TeamAdminInfo", TeamAdminInfo);
            //Modal.Show<RevokeInvitationComp>(string.Empty, parameters);
        }
    }
}
