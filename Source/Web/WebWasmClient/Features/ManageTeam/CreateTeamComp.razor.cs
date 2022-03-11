using Blazored.Modal;
using Common.Identity.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;

namespace WebWasmClient.Features.ManageTeam
{
    public partial class CreateTeamComp
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        private TeamDTO team;
        private IBrowserFile browserFile;
        private string picturePreview;
        private string base64;
        private bool valid;
        private string currentName = string.Empty;
        public string CurrentName
        {
            get => currentName;
            set
            {
                currentName = value;
                valid = ValidationService.Validate(new TeamDTO { Name = currentName }).IsValid;
            }
        }

        protected override async Task OnInitializedAsync()
        {
            team = new TeamDTO();
        }
        private async Task LoadFiles(InputFileChangeEventArgs e)
        {
            IBrowserFile imgFile = e.File;
            var buffers = new byte[imgFile.Size];
            await imgFile.OpenReadStream().ReadAsync(buffers);
            string imageType = imgFile.ContentType;
            base64 = Convert.ToBase64String(buffers);
            picturePreview = $"data:{imageType};base64,{Convert.ToBase64String(buffers)}";
        }
        private async Task CreateGroupAsync()
        {
            await HttpClientService.PostToAPIAsync("/team", team);
            await ModalInstance.CloseAsync();
            NavigationManager.NavigateTo("/", true);
        }
    }
}
