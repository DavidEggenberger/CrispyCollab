using Common.Kernel;
using Infrastructure.EFCore;
using Infrastructure.EFCore.Configuration;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MultiTenancy
{
    public class MultiTenantDbContext<T> : DbContext where T : DbContext
    {
        private readonly ITenantResolver tenantResolver;
        private readonly IUserResolver userResolver;
        protected readonly Guid tenantId;

        public MultiTenantDbContext(DbContextOptions<T> dbContextOptions, IServiceProvider serviceProvider) : base(dbContextOptions)
        {
            tenantResolver = serviceProvider.GetRequiredService<ITenantResolver>();
            userResolver = serviceProvider.GetRequiredService<IUserResolver>();
            tenantId = tenantResolver.ResolveTenantId();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ThrowIfDbSetEntityNotTenantIdentifiable(modelBuilder);

            modelBuilder.ApplyBaseEntityConfiguration(tenantResolver.ResolveTenantId());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId(tenantResolver.ResolveTenantId());
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

        private void SetTenantId(Guid teamId)
        {
            foreach (var entry in ChangeTracker.Entries<ITenantIdentifiable>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TenantId = teamId;
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
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if(entityType is not ITenantIdentifiable)
                {
                    throw new Exception();
                }
            }
        }
    }
}
