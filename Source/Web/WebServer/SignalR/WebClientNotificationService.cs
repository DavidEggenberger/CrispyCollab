using Infrastructure.Identity.Interfaces;
using System;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    public class WebClientNotificationService : IWebClientNotificationService
    {
        public Task NotifyAdminTeamMembers(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyAllTeamMembers(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public Task NotifyUser(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
