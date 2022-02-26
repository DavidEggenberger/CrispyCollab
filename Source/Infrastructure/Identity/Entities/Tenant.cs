using Domain.SharedKernel;
using Infrastructure.Identity.Entities;
using Infrastructure.Identity.Types.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public byte[] IconData { get; set; }
        public string NameIdentitifer { get; set; }
        public TenantPlan Plan { get; set; }
        public List<TenantInvitedUser> InvitedUsers { get; set; }
        public List<ApplicationUserTenant> Members { get; set; }   
    }
}
