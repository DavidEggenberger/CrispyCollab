using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using WebServer.Hubs;

namespace WebServer.SignalR
{
    public class AggregatesUINotifierService
    {
        private readonly IHubContext<NotificationHub> hubContext;
        public AggregatesUINotifierService(IHubContext<NotificationHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task UpdateChannels(Guid teamId)
        {
            await hubContext.Clients.Group($"{teamId}").SendAsync("UpdateChannels");
        }
    }
}
