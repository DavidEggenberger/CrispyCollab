using Blazored.Modal;
using Common.Identity.Team.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Features.ManageTeam.Members.Modals
{
    public partial class InviteMemberComp
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        private List<string> emailAddresses;
        private bool valid;
        private string _currentMail = string.Empty;
        public string currentMail
        {
            get => _currentMail;
            set
            {
                _currentMail = value;
                valid = ValidationService.Validate(new InviteTeamMembersDTO { Emails = new List<string> { _currentMail } }).IsValid;
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
            InviteTeamMembersDTO inviteUserToTeamDTO = new InviteTeamMembersDTO()
            {
                Emails = emailAddresses
            };
            await HttpClientService.PostToAPIAsync("/teammember/invite", inviteUserToTeamDTO);
            await ModalInstance.CloseAsync();
        }
    }
}
