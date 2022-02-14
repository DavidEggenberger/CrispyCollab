using Domain.SharedKernel;
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
        public string IconUri { get; set; }
        public string Name { get; set; }
        public TenantPlan Plan { get; set; }
        public List<TenantUser> Members { get; set; }   
    }
}
