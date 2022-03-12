using Domain.Interfaces;
using Identity.Interfaces;
using Infrastructure.Identity;
using System;
using System.Threading.Tasks;

namespace WebServer.Hubs
{
    public class IdentityUINotifierService : IIdentityUINotifierService
    {
        public async Task NotifyAdminMembersAboutNewNotification(Guid teamId)
        {
            //throw new NotImplementedException();
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

        public Task NotifyUserAboutInvitation(Guid userId, ApplicationUserTeam invitation)
        {
            throw new NotImplementedException();
        }
    }
}
