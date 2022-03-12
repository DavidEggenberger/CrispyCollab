using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Interfaces
{
    public interface IIdentityUINotifierService
    {
        Task NotifyAdminMembersAboutNewNotification(Guid teamId);
        Task NotifyAllTeamMembers(Guid teamId);
        Task NotifyUserAboutInvitation(Guid userId, ApplicationUserTeam invitation);
    }
}
