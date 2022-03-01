using Infrastructure.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.EFCore.Configuration
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasMany(x => x.SubscribedTeams)
                .WithOne(x => x.SubscriptionPlan)
                .HasForeignKey(x => x.SubscriptionPlanId);
        }
    }
}
