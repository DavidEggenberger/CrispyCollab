using Microsoft.AspNetCore.Components;
using Modules.Channels.Public.DTOs.ChannelAggregate;

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
            //        Channel = await HttpWeb.ClientService.GetFromAPIAsync<ChannelDTO>($"/channel/{Channel.Id}");
            //        StateHasChanged();
            //    }
            //});
        }

        protected async Task DeleteChannel()
        {
            //await HttpWeb.ClientService.DeleteFromAPIAsync("/channel", Channel.Id);
        }
    }
}
