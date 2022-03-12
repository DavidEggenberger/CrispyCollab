using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class NotificationManager
    {
        private readonly IdentificationDbContext identificationDbContext;
        public NotificationManager(IdentificationDbContext identificationDbContext)
        {
            this.identificationDbContext = identificationDbContext;
        }

        public async Task CreateNotification(ApplicationUser user, Team team, NotificationType notificationType, string message, TeamRole visibleTo)
        {
            identificationDbContext.Notifications.Add(new Notification
            {
                CreatedAt = DateTime.Now,
                Creator = user,
                Message = message,
                Team = team,
                Type = notificationType,
                VisibleTo = visibleTo 
            });
            await identificationDbContext.SaveChangesAsync();   
        }
    }
}
