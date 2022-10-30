using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Modules.ChannelModule.Web.DTOs;
using Shared.Web.Client;
using Shared.Web.Client.Services;

namespace WebWasmClient.Pages
{
    public partial class ChannelsBase : BaseComponent
    {
        protected bool loading = true;
        protected List<ChannelDTO> channels;
        string channelName;
        protected override async Task OnInitializedAsync()
        {
            //HubConnection.On("UpdateChannels", async () =>
            //{
            //    channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");
            //    StateHasChanged();
            //});
            
            channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");

            loading = false;
        }

        protected async Task CreateChannel(string name)
        {
            var createChannel = new CreateChannelCommandDTO
            {
                Name = name
            };
            await HttpClientService.PostToAPIAsync<CreateChannelCommandDTO>("/channel", createChannel);
            channelName = string.Empty;
        }

        protected async Task ChangeChannelName(string name)
        {
            var changeChannelName = new ChangeChannelNameDTO
            {
                NewName = name
            };
            await HttpClientService.PostToAPIAsync<ChangeChannelNameDTO>("/channel", changeChannelName);
        }

        private async Task SendMessage(string message)
        {
        }
    }
}
