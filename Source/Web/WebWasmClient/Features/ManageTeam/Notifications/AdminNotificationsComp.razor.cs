using Common.Identity.Team.AdminManagement;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace WebWasmClient.Features.ManageTeam.Notifications
{
    public partial class AdminNotificationsComp
    {
        [Parameter] public List<AdminNotificationDTO> AdminNotifications { get; set; }
    }
}
