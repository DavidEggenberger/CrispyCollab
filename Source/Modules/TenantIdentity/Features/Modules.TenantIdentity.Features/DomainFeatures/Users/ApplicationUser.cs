using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shared.Features.Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Modules.TenantIdentity.Features.DomainFeatures.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string PictureUri { get; set; }
        public bool IsOnline => CountOfOpenTabs > 0;
        public int CountOfOpenTabs { get; set; }
        public string StripeCustomerId { get; set; }
        public Guid SelectedTenantId { get; set; }

        public void IncrementOpenTabCount()
        {
            CountOfOpenTabs++;
        }

        public void DecrementOpenTabCount()
        {
            if (CountOfOpenTabs == 0)
            {
                throw new DomainException("User has no tabs to close");
            }
            CountOfOpenTabs--;
        }

        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; set; }
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; set; }
        public virtual ICollection<IdentityUserToken<Guid>> Tokens { get; set; }
    }

    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

        }
    }
}
