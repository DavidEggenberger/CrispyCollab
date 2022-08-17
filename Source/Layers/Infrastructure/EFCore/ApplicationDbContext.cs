using Domain.Aggregates.ChannelAggregate;
using Domain.Aggregates.TenantAggregate;
using Domain.SharedKernel;
using Infrastructure.CQRS.DomainEvent;
using Infrastructure.Identity;
using Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore
{
    public class ApplicationDbContext : MultiTenantDbContext<ApplicationDbContext>
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        public ApplicationDbContext(IDomainEventDispatcher domainEventDispatcher, DbContextOptions<ApplicationDbContext> dbContextOptions, IServiceProvider serviceProvider) : base(dbContextOptions, serviceProvider)
        {
            this.domainEventDispatcher = domainEventDispatcher;
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Tenant> Tenants { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedRowCount = await base.SaveChangesAsync(cancellationToken);
            await DispatchEventsAsync(cancellationToken);
            return changedRowCount;
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
    }
}
