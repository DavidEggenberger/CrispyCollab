using Infrastructure.Identity.Types.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Entities
{
    public class AdminNotification
    {
        public Guid Id { get; set; }
        public AdminNotificationType Type { get; set; }
        public string Message { get; set; }
        public Guid CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
