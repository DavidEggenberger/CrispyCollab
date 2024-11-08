﻿using Modules.IdentityModule.Shared;

namespace WebShared.Identity.Team.AdminManagement
{
    public class AdminNotificationDTO
    {
        public Guid Id { get; set; }
        public AdminNotificationTypeDTO Type { get; set; }
        public string Message { get; set; }
        public UserDTO Creator { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
