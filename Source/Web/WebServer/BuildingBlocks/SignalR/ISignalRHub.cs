using Microsoft.AspNetCore.SignalR;

namespace Shared.Modules.Layers.Features.SignalR
{
    public interface ISignalRHub
    {
        public IHubCallerClients Clients { get; set; }
        public HubCallerContext Context { get; set; }
        public IGroupManager Groups { get; set; }
    }
}
