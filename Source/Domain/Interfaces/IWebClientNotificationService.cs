using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Interfaces
{
    public interface IWebClientNotificationService
    {
        Task NotifyAdminTeamMembers(Guid teamId);
        Task NotifyAllTeamMembers(Guid teamId);
        Task NotifyUser(Guid userId);
    }
}
