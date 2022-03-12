using Infrastructure.Identity.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class Notification
    {
        public Guid Id { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public Guid CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public TeamRole VisibleTo { get; set; }
        public DateTime CreatedAt { get; set; }
        private Notification() {}
        public Notification(Team team, TeamRole visibleTo, NotificationType notificationType, ApplicationUser creator)
        {
            Team = team;
            VisibleTo = visibleTo;
            Type = notificationType;
            Creator = creator;
            Message = notificationType switch
            {
                NotificationType.SubscriptionCreated => $"{Creator.UserName} created a subscription for {Team.Name}",

            };
        }
    }
}
