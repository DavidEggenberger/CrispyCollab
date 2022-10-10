using Microsoft.AspNetCore.Identity;

namespace Modules.IdentityModule.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PictureUri { get; set; }
        public bool IsOnline { get; set; }
        public int TabsOpen { get; set; }
        public string StripeCustomerId { get; set; }
        public Guid SelectedTenantId { get; set; }

        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    }
}
