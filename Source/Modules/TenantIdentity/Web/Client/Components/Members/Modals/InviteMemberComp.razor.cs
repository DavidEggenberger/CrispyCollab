using Blazored.Modal;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Shared.Client;
using WebShared.Identity.Team.DTOs;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class InviteMemberCompBase : BaseComponent
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        protected List<string> emailAddresses;
        protected bool valid;
        private string _currentMail = string.Empty;
        public string currentMail
        {
            get => _currentMail;
            set
            {
                _currentMail = value;
                valid = ValidationService.Validate(new InviteMembersDTO { Emails = new List<string> { _currentMail } }).IsValid;
            }
        }
        protected override async Task OnInitializedAsync()
        {
            emailAddresses = new List<string>();
        }
        public async Task OnKeyDown(KeyboardEventArgs args)
        {
            if (args.Key == "Enter" && valid)
            {
                emailAddresses.Add(currentMail);
                currentMail = string.Empty;
                StateHasChanged();
            }
        }

        public async Task InviteUsersAsync()
        {
            InviteMembersDTO inviteUserToTeamDTO = new InviteMembersDTO()
            {
                Emails = emailAddresses
            };
            await HttpClientService.PostToAPIAsync("/teamadmin/invite", inviteUserToTeamDTO);
            await ModalInstance.CloseAsync();
        }
    }
}
