using Modules.Channels.Public.DTOs.ChannelAggregate;
using Shared.Client;

namespace Web.Client.Pages
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
            //    channels = await HttpWeb.ClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");
            //    StateHasChanged();
            //});
            
            channels = await httpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");

            loading = false;
        }

        protected async Task CreateChannel(string name)
        {
            var createChannel = new ChannelDTO
            {
                Name = name
            };
            await httpClientService.PostToAPIAsync<ChannelDTO>("/channel", createChannel);
            channelName = string.Empty;
        }

        protected async Task ChangeChannelName(string name)
        {
            var changeChannelName = new ChannelDTO
            {
                Name = name
            };
            await httpClientService.PostToAPIAsync<ChannelDTO>("/channel", changeChannelName);
        }

        private async Task SendMessage(string message)
        {
        }
    }
}
