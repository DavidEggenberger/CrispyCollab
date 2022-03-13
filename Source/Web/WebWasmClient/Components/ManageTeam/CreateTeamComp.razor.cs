using Blazored.Modal;
using Common.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebWasmClient.Services;

namespace WebWasmClient.Features.ManageTeam
{
    public partial class CreateTeamComp
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        private TeamDTO team = new TeamDTO();
        private ValidationServiceResult validationServiceResult;
        private string currentName = string.Empty;
        public string CurrentName
        {
            get => currentName;
            set
            {
                currentName = value;
                validationServiceResult = ValidationService.Validate(new TeamDTO { Name = currentName });
            }
        }
        private async Task CreateGroupAsync()
        {
            await HttpClientService.PostToAPIAsync("/team", new TeamDTO { Name = currentName });
            await ModalInstance.CloseAsync();
            NavigationManager.NavigateTo("/", true);
        }
    }
}
