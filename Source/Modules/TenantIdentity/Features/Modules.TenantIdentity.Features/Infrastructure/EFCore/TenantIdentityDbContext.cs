using Modules.TenantIdentity.Domain;

namespace Modules.TenantIdentity.Features.EFCore
{
    public class TenantIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        private readonly IConfiguration configuration;
        public TenantIdentityDbContext(IConfiguration configuration, DbContextOptions<IdentityDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        //public TenantIdentityDbContext(DbContextOptions<TenantIdentityDbContext> dbContextOptions, IServiceProvider serviceProvider, IConfiguration configuration) : base(dbContextOptions, serviceProvider, configuration)
        //{

        //}
        public DbSet<Tenant> Tenants { get; set; }
    }
}
