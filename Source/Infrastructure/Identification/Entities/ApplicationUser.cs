using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identification
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<TenantApplicationUser> Memberships { get; set; }
    }
}
