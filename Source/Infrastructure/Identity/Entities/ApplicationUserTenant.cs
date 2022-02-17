using Domain.SharedKernel.Attributes;
using Infrastructure.Identity.Types.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUserTenant 
    {
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public TenantRoleType Role { get; set; }
        public TenantStatus Status { get; set; }
    }
}
