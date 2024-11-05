using Blazored.Modal;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Shared;
using Shared.Client;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class CreateTenantComponentBase : BaseComponent
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        protected TeamDTO team = new TeamDTO();
        protected ValidationResult validationServiceResult;
        private string currentName = string.Empty;
        public string CurrentName
        {
            get => currentName;
            set
            {
                currentName = value;
                validationServiceResult = validationService.Validate(new TeamDTO { Name = currentName });
            }
        }
        private async Task CreateGroupAsync()
        {
            await httpClientService.PostToAPIAsync("/team", new TeamDTO { Name = currentName });
            await ModalInstance.CloseAsync();
            navigationManager.NavigateTo("/", true);
        }
    }
}
