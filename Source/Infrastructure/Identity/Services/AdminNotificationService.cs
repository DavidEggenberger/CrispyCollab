using Identity.Interfaces;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AdminNotificationService
    {
        private readonly IdentificationDbContext identificationDbContext;
        private readonly IIdentityUINotifierService identityUINotifierService;
        public AdminNotificationService(IdentificationDbContext identificationDbContext, IIdentityUINotifierService identityUINotifierService)
        {
            this.identityUINotifierService = identityUINotifierService;
            this.identificationDbContext = identificationDbContext;
        }

        public async Task CreateNotification(Team team, NotificationType notificationType, ApplicationUser creator, Subscription subscription = null)
        {
            AdminNotification notification = new AdminNotification()
            {
                Team = team,
                Creator = creator,
                Type = notificationType
            };
            notification.Message = notificationType switch
            {

            };
            identificationDbContext.AdminNotifications.Add(new AdminNotification(team, notificationType, creator));
            await identityUINotifierService.NotifyAdminMembersAboutNewNotification(team.Id);
        }
    }
}
