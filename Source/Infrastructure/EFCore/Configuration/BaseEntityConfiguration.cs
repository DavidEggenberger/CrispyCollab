using Domain.SharedKernel;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configuration
{
    public static class BaseEntityConfiguration
    {
        static void Configure<TEntity, T>(ModelBuilder modelBuilder, Guid teamId)
            where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasQueryFilter(x => x.TeamId == teamId);
                builder.Property(e => e.RowVersion).IsConcurrencyToken();
            });
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder, Guid teamId)
        {
            var method = typeof(BaseEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(Configure));
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is Entity))
            {
                method.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder, teamId });
            }
            return modelBuilder;
        }
    }
}
