using Modules.Channels.Web.Shared.DTOs.ChannelAggregate;
using Shared.Client;

namespace Client.Pages
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
            var createChannel = new ChannelDTO
            {
                Name = name
            };
            await HttpClientService.PostToAPIAsync<ChannelDTO>("/channel", createChannel);
            channelName = string.Empty;
        }

        protected async Task ChangeChannelName(string name)
        {
            var changeChannelName = new ChannelDTO
            {
                Name = name
            };
            await HttpClientService.PostToAPIAsync<ChannelDTO>("/channel", changeChannelName);
        }

        private async Task SendMessage(string message)
        {
        }
    }
}
