using WebShared.Features.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace WebWasmClient.Pages
{
    public partial class Channels
    {
        private bool loading = true;
        private List<ChannelDTO> channels;
        private string channelName;
        protected override async Task OnInitializedAsync()
        {
            HubConnection.On("UpdateChannels", async () =>
            {
                channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");
                StateHasChanged();
            });
            
            channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");

            loading = false;
        }

        private async Task CreateChannel(string name)
        {
            var createChannel = new CreateChannelCommandDTO
            {
                Name = name
            };
            await HttpClientService.PostToAPIAsync<CreateChannelCommandDTO>("/channel", createChannel);
            channelName = string.Empty;
        }

        private async Task ChangeChannelName(string name)
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
