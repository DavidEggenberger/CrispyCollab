using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EFCore.Configuration
{
    public class ApplicationUserTenantConfiguration : IEntityTypeConfiguration<ApplicationUserTenant>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserTenant> builder)
        {
            builder.Navigation(x => x.Tenant).AutoInclude();
        }
    }
}
