using Domain.Aggregates.ChannelAggregate;
using Domain.Aggregates.TopicAggregate;
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

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        private readonly ITeamResolver teamResolver;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions, IDomainEventDispatcher domainEventDispatcher, ITeamResolver teamResolver) : base(dbContextOptions)
        {
            this.domainEventDispatcher = domainEventDispatcher;
            this.teamResolver = teamResolver;   
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Channel> Channels { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
            foreach (var entity in Model.GetEntityTypes())
            {
                var method = EntityConfiguration.ConfigureEntity.MakeGenericMethod(entity.GetType());
                method.Invoke(this, new object[] { modelBuilder, teamResolver.ResolveTeamId() });
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAutitableEntities();
            int result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEventsAsync(cancellationToken);
            return result;
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
    }
}
