using Domain.Aggregates.ChannelAggregate;
using Domain.Aggregates.TopicAggregate;
using Domain.Kernel;
using Domain.SharedKernel;
using Infrastructure.CQRS.DomainEvent;
using Infrastructure.EFCore.Configuration;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EFCore
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        private readonly ITeamResolver teamResolver;
        private readonly IUserResolver userResolver;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IDomainEventDispatcher domainEventDispatcher, ITeamResolver teamResolver, IUserResolver userResolver) : base(dbContextOptions)
        {
            this.domainEventDispatcher = domainEventDispatcher;
            this.teamResolver = teamResolver;
            this.userResolver = userResolver;
        }

        public DbSet<Channel> Channels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyBaseEntityConfiguration(teamResolver.ResolveTeamId());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly, 
                x => x.Namespace == "Infrastructure.EFCore.Configuration.ChannelAggregate");
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfMultipleTenants();
            UpdateAutitableEntities();
            SetTeamId(teamResolver.ResolveTeamId());
            UpdateCreatedByUserEntities(userResolver.GetIdOfLoggedInUser());
            await DispatchEventsAsync(cancellationToken);
            return await base.SaveChangesAsync(cancellationToken);
        }
        private async Task DispatchEventsAsync(CancellationToken cancellationToken)
        {
            var domainEntities = ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToList();

            foreach (var entity in domainEntities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearDomainEvents();
                foreach (var domainEvent in events)
                {
                    await domainEventDispatcher.DispatchAsync(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }
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
        private void SetTeamId(Guid teamId)
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
        private void ThrowIfMultipleTenants()
        {
            var ids = (from e in ChangeTracker.Entries()
                   where e.Entity is Entity
                   select ((Entity)e.Entity).TeamId)
                   .Distinct()
                   .ToList();
            
            if(ids.Count == 0)
            {
                return;
            }
 
            if(ids.Count > 1)
            {
                throw new CrossTenantUpdateException(ids);
            }
 
            if(ids.First() != teamResolver.ResolveTeamId())
            {
                throw new CrossTenantUpdateException(ids);
            }
        }
    }
}
