using Infrastructure.Identity.WebClientNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IWebClientNotificationService
    {
        Task NotifyAdminTeamMembers(Team team, WebClientNotificationType notificationType);
        Task NotifyAllTeamMembers(Team team, WebClientNotificationType notificationType);
        Task NotifyUser(ApplicationUser user, WebClientNotificationType notificationType);
    }
}
