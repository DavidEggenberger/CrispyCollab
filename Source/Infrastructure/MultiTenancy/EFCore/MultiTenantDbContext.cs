using Domain.Kernel;
using Domain.SharedKernel;
using Infrastructure.EFCore;
using Infrastructure.EFCore.Configuration;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.MultiTenancy
{
    public class MultiTenantDbContext<T> : DbContext where T : DbContext
    {
        private readonly ITenantResolver teamResolver;
        private readonly IUserResolver userResolver;
        protected readonly Guid tenantId;

        public MultiTenantDbContext(DbContextOptions<T> dbContextOptions, IServiceCollection serviceCollection) : base(dbContextOptions)
        {
            var seriveProvider = serviceCollection.BuildServiceProvider();
            teamResolver = seriveProvider.GetRequiredService<ITenantResolver>();
            userResolver = seriveProvider.GetRequiredService<IUserResolver>();
            tenantId = teamResolver.ResolveTenant();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyBaseEntityConfiguration(teamResolver.ResolveTenant());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTenantId(teamResolver.ResolveTenant());
            UpdateCreatedByUserEntities(userResolver.GetIdOfLoggedInUser());
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAutitableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<Entity>())
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
            foreach (var entry in ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.CreatedByUserId = userId;
            }
            foreach (var entry in ChangeTracker.Entries<ValueObject>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.CreatedByUserId = userId;
            }
        }

        private void SetTenantId(Guid teamId)
        {
            foreach (var entry in ChangeTracker.Entries<Entity>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TeamId = teamId;
            }
            foreach (var entry in ChangeTracker.Entries<ValueObject>().Where(x => x.State == EntityState.Added))
            {
                entry.Entity.TeamId = teamId;
            }
        }

        private void ThrowIfMultipleTenants()
        {
            var ids = (from e in ChangeTracker.Entries()
                       where e.Entity is Entity
                       select ((Entity)e.Entity).TeamId)
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
    }
}
