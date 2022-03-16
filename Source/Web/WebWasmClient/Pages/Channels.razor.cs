using Common.Features.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Pages
{
    public partial class Channels
    {
        private List<ChannelDTO> channels;
        protected override async Task OnInitializedAsync()
        {
            channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/api/channel");
        }
    }
}
