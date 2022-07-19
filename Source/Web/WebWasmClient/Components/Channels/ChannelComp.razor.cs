using WebShared.Features.Channel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Components.Channels
{
    public partial class ChannelComp
    {
        [Parameter] public ChannelDTO Channel { get; set; }
        protected override async Task OnInitializedAsync()
        {
            HubConnection.On<Guid>("UpdateChannel", async (channelId) =>
            {
                if(Channel.Id == channelId)
                {
                    Channel = await HttpClientService.GetFromAPIAsync<ChannelDTO>($"/channel/{Channel.Id}");
                    StateHasChanged();
                }
            });
        }

        private async Task DeleteChannel()
        {
            await HttpClientService.DeleteFromAPIAsync("/channel", Channel.Id);
        }
    }
}
