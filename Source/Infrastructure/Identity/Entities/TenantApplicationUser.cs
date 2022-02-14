using Domain.SharedKernel.Attributes;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class TenantApplicationUser 
    {
        public Guid Id { get; set; }
        public TenantRoleType Role { get; set; }
        public Guid TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
