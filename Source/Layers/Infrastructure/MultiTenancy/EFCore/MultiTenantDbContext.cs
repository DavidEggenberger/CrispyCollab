using Common.Kernel;
using Domain.SharedKernel.Attributes;
using Infrastructure.EFCore;
using Infrastructure.EFCore.Configuration;
using Infrastructure.Identity;
using Infrastructure.Interfaces;
using Infrastructure.MultiTenancy.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.MultiTenancy
{
    public class MultiTenantDbContext<T> : DbContext where T : DbContext
    {
        private readonly ITenantResolver tenantResolver;
        private readonly IUserResolver userResolver;
        private readonly Guid tenantId;

        public MultiTenantDbContext(DbContextOptions<T> dbContextOptions, IServiceProvider serviceProvider) : base(dbContextOptions)
        {
            tenantResolver = serviceProvider.GetRequiredService<ITenantResolver>();
            userResolver = serviceProvider.GetRequiredService<IUserResolver>();
            tenantId = tenantResolver.CanResolveTenant() is true ? tenantResolver.ResolveTenantId() : Guid.NewGuid();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ThrowIfDbSetEntityNotTenantIdentifiable(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly,
                x => x.Namespace == "Infrastructure.EFCore.Configuration.ChannelAggregate");
            modelBuilder.ApplyBaseEntityConfiguration(tenantId);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId();
            UpdateCreatedByUserEntities(userResolver.GetIdOfLoggedInUser());
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAutitableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastUpdated = DateTime.Now;
                        break;
                }
            }
        }

        private void UpdateCreatedByUserEntities(Guid userId)
        {
            foreach (var entry in ChangeTracker.Entries<IAuditable>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.CreatedByUserId = userId;
            }
        }

        private void SetTenantId()
        {
            foreach (var entry in ChangeTracker.Entries<ITenantIdentifiable>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TenantId = tenantId;
            }
        }

        private void ThrowIfMultipleTenants()
        {
            var ids = ChangeTracker.Entries()
                    .Where(e => e.Entity is ITenantIdentifiable)
                    .Select(e => (e.Entity as ITenantIdentifiable).TenantId)
                    .Distinct()
                    .ToList();

            if (ids.Count == 0)
            {
                return;
            }

            if (ids.Count > 1)
            {
                throw new CrossTenantUpdateException(ids);
            }

            if (ids.First() != tenantId)
            {
                throw new CrossTenantUpdateException(ids);
            }
        }

        private void ThrowIfDbSetEntityNotTenantIdentifiable(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(t => t.ClrType.GetCustomAttribute<AggregateRootAttribute>() is not null))
            {
                if(typeof(ITenantIdentifiable).IsAssignableFrom(entityType.ClrType) is false)
                {
                    throw new EntityNotTenantIdentifiableException(entityType.ClrType.Name);
                }
            }
        }
    }
}
