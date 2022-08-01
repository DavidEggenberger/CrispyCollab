using Domain.Interfaces;
using Identity.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    public class IdentityUINotifierService : IIdentityUINotifierService
    {
        private readonly IHubContext<NotificationHub> notificationHubContext;
        public IdentityUINotifierService(IHubContext<NotificationHub> notificationHubContext)
        {
            this.notificationHubContext = notificationHubContext;
        }

        public async Task NotifyAdminMembersAboutNewNotification(Guid teamId)
        {
            await notificationHubContext.Clients.Group($"{teamId}{TeamRole.Admin}").SendAsync("UpdateAdminInformation");
        }

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

        //public Task NotifyUserAboutInvitation(Guid userId, ApplicationUserTeam invitation)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
