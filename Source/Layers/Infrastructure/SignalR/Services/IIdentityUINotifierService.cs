using Infrastructure.Identity;

namespace Identity.Interfaces
{
    public interface IIdentityUINotifierService
    {
        Task NotifyAdminMembersAboutNewNotification(Guid teamId);
        Task NotifyAllTeamMembers(Guid teamId);
        //Task NotifyUserAboutInvitation(Guid userId, ApplicationUserTeam invitation);
    }
}
