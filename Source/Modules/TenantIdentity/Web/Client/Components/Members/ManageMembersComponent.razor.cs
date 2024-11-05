using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Shared;
using Shared.Client;

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
