using Microsoft.AspNetCore.Components;
using Modules.Channels.Web.Shared.DTOs.ChannelAggregate;
using Shared.Client;

namespace Modules.Channels.Web.Client.Components
{
    public partial class ChannelComponent
    {
        [Parameter] public ChannelDTO Channel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //HubConnection.On<Guid>("UpdateChannel", async (channelId) =>
            //{
            //    if(Channel.Id == channelId)
            //    {
            //        Channel = await HttpClientService.GetFromAPIAsync<ChannelDTO>($"/channel/{Channel.Id}");
            //        StateHasChanged();
            //    }
            //});
        }

        protected async Task DeleteChannel()
        {
            //await HttpClientService.DeleteFromAPIAsync("/channel", Channel.Id);
        }
    }
}
