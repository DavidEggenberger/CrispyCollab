using Blazored.Modal;
using Common.Features.Channel.Commands;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebWasmClient.Services;

namespace WebWasmClient.Components.Channels.Modals
{
    public partial class CreateChannelComp
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        private CreateChannelDTO team = new CreateChannelDTO();
        private ValidationServiceResult validationServiceResult;
        private string currentName = string.Empty;
        public string CurrentName
        {
            get => currentName;
            set
            {
                currentName = value;
                validationServiceResult = ValidationService.Validate(new CreateChannelDTO { Name = currentName });
            }
        }
        private async Task CreateChannelAsync()
        {
            await HttpClientService.PostToAPIAsync("/channel", new CreateChannelDTO { Name = currentName });
            await ModalInstance.CancelAsync();
        }
    }
}
