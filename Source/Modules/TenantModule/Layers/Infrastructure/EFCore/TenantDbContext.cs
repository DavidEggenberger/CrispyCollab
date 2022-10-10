using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Modules.TenantModule.Domain;

namespace Modules.TenantModule.Infrastructure.EFCore
{
    public class TenantDbContext : BaseDbContext<TenantDbContext>
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        {

        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
