using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Features.DomainKernel;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Features.MultiTenancy.EFCore;
using Shared.Kernel.BuildingBlocks.Authorization.Services;
using Shared.Features.CQRS.DomainEvent;

namespace Shared.Features.EFCore
{
    public class BaseDbContext<T> : MultiTenantDbContext<T> where T : DbContext
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        public BaseDbContext(DbContextOptions<T> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {
            this._domainEventDispatcher = serviceProvider.GetRequiredService<IDomainEventDispatcher>();
        }

        public IUserAuthorizationService TenantAuthorizationService { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var changedRowCount = await base.SaveChangesAsync(cancellationToken);
            await DispatchEventsAsync(cancellationToken);
            return changedRowCount;
        }
        private async Task DispatchEventsAsync(CancellationToken cancellationToken)
        {
            var domainKernelEntities = ChangeTracker
                .Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToList();

            foreach (var entity in domainKernelEntities)
            {
                var events = entity.DomainEvents.ToArray();
                entity.ClearEvents();
                foreach (var domainEvent in events)
                {
                    await _domainEventDispatcher.RaiseAsync(domainEvent, cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}
