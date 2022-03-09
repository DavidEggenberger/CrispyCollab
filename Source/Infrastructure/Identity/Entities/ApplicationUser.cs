using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string StripeCustomerId { get; set; }
        public Team SelectedTeam { get; set; }
        public List<Team> CreatedTeams { get; set; }

        private List<ApplicationUserTeam> memberships = new List<ApplicationUserTeam>();
        public IReadOnlyCollection<ApplicationUserTeam> Memberships => memberships.AsReadOnly();
        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    }
}
