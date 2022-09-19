using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Query
{
    public class BaseQueryHandler<T> where T : class
    {
        public DbSet<T> dbSet { get; set; }
        public BaseQueryHandler(ApplicationDbContext applicationDbContext)
        {
            dbSet = applicationDbContext.Set<T>();
        }
    }
}
