using Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identification
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string IconUri { get; set; }
        public string Name { get; set; }
        public TenantPlan Plan { get; set; }
        public List<TenantApplicationUser> Members { get; set; }
    }
}
