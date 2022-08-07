using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR
{
    public interface ISignalRHub
    {
        public IHubCallerClients Clients { get; set; }
        public HubCallerContext Context { get; set; }
        public IGroupManager Groups { get; set; }
    }
}
