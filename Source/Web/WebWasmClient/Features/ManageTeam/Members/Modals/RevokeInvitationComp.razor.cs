using Blazored.Modal;
using Common.Identity.Team;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace WebWasmClient.Features.ManageTeam.Members.Modals
{
    public partial class RevokeInvitationComp
    {
        [Parameter] public MemberDTO Member { get; set; }
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }

        public async Task RevokeInvitation()
        {

        }
        public async Task CloseModal()
        {
            await ModalInstance.CloseAsync();
        }
    }
}
