using Domain.Aggregates.ChannelAggregate;
using Domain.SharedKernel;
using Infrastructure.CQRS.DomainEvent;
using Infrastructure.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.EFCore
{
    public class ApplicationDbContext : MultiTenantDbContext<ApplicationDbContext>
    {
        private readonly IDomainEventDispatcher domainEventDispatcher;
        public ApplicationDbContext(IDomainEventDispatcher domainEventDispatcher, DbContextOptions<ApplicationDbContext> dbContextOptions, IServiceCollection serviceCollection) : base(dbContextOptions, serviceCollection)
        {
            this.domainEventDispatcher = domainEventDispatcher;
        }

        public DbSet<Channel> Channels { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly, 
                x => x.Namespace == "Infrastructure.EFCore.Configuration.ChannelAggregate");
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
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
    }
}
