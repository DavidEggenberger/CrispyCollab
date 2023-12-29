using Microsoft.AspNetCore.Components;
using Modules.IdentityModule.Web.DTOs;

namespace Modules.TenantIdentity.Web.Client.Components
{
    public partial class CreateTenantComponentBase : BaseComponent
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        protected TeamDTO team = new TeamDTO();
        protected ValidationServiceResult validationServiceResult;
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
