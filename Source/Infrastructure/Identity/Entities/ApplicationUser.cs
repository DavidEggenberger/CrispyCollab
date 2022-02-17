using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PictureUri { get; set; }
        public bool IsOnline { get; set; }
        public int TabsOpen { get; set; }
        public List<ApplicationUserTenant> Memberships { get; set; }
        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    }
}
