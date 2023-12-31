using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Shared.Features.DomainKernel;
using Shared.Features.DomainKernel.Attributes;

namespace Shared.Features.MultiTenancy.EFCore
{
    public static class MultiTenancyEntityConfiguration
    {
        static void ConfigureAggregateRoot<TAggregateRoot>(ModelBuilder modelBuilder, Guid teamId)
           where TAggregateRoot : Entity
        {
            modelBuilder.Entity<TAggregateRoot>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TAggregateRoot>>)(builder =>
            {
                builder.HasQueryFilter((TAggregateRoot x) => x.TenantId == teamId);
            }));
        }

        static void ConfigureEntity<TEntity, T>(ModelBuilder modelBuilder)
            where TEntity : Entity
        {
            modelBuilder.Entity<TEntity>((Action<Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TEntity>>)(builder =>
            {
                builder.HasKey((System.Linq.Expressions.Expression<Func<TEntity, object?>>)(x => x.TenantId));
                builder.Property<byte[]>(e => e.RowVersion).IsConcurrencyToken();
            }));
        }

        public static ModelBuilder ApplyBaseEntityConfiguration(this ModelBuilder modelBuilder, Guid teamId)
        {
            var configureAggregateRootMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureAggregateRoot));
            var configureEntityMethod = typeof(MultiTenancyEntityConfiguration).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(ConfigureEntity));


            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x.GetType().GetCustomAttribute(typeof(AggregateRootAttribute)) != null))
            {
                configureAggregateRootMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder, teamId });
            }
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(x => x is Entity))
            {
                configureEntityMethod.MakeGenericMethod(entityType.ClrType, entityType.GetType()).Invoke(null, new object[] { modelBuilder });
            }

            return modelBuilder;
        }
    }
}
