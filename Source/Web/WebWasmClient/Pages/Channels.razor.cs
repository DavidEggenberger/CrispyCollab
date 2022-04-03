using Common.Features.Channel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebWasmClient.Pages
{
    public partial class Channels
    {
        private bool loading = true;
        private List<ChannelDTO> channels;
        protected override async Task OnInitializedAsync()
        {
            channels = await HttpClientService.GetFromAPIAsync<List<ChannelDTO>>("/channel");

            loading = false;
        }
    }
}
