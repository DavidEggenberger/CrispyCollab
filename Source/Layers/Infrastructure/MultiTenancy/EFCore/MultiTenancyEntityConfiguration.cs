using Domain.Kernel;
using Domain.SharedKernel;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.EFCore.Configuration
{
    public static class MultiTenancyEntityConfiguration
    {
        static void ConfigureEntity<TEntity, T>(ModelBuilder modelBuilder, Guid teamId)
            where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasQueryFilter(x => x.TenantId == teamId);
                builder.Property(e => e.RowVersion).IsConcurrencyToken();
            });
        }

        static void ConfigureValueObject<TValueObject, T>(ModelBuilder modelBuilder, Guid teamId)
            where TValueObject : ValueObject
        {
            modelBuilder.Entity<TValueObject>(builder =>
            {
                builder.HasQueryFilter(x => x.TenantId == teamId);
            });
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder, Guid teamId)
        {
            var configureEntityMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureEntity));
            var configureValueObject = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureValueObject));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is Entity))
            {
                configureEntityMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder, teamId });
            }
            foreach (var valueObject in modelBuilder.Model.GetEntityTypes().Where(x => x is ValueObject))
            {
                configureEntityMethod.MakeGenericMethod(valueObject.ClrType, valueObject.GetType()).Invoke(null, new object[] { modelBuilder, teamId });
            }
            return modelBuilder;
        }
    }
}
