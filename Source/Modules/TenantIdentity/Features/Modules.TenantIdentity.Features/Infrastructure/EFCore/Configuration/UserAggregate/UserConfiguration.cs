using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.TenantIdentity.Features.DomainFeatures.UserAggregate;

namespace Modules.TenantIdentity.Features.Infrastructure.EFCore.Configuration.UserAggregate
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

        }
    }
}
