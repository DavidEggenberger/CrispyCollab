using Domain.SharedKernel;
using Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EFCore.Configuration
{
    public class EntityBaseConfiguration
    {
        public static readonly MethodInfo ConfigureEntity = 
            typeof(ApplicationDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Single(t => t.IsGenericMethod && t.Name == nameof(SetEntityConfiguration));
        public void SetEntityConfiguration<T>(ModelBuilder builder, Guid teamId) where T : Entity
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().Property(e => e.RowVersion).IsConcurrencyToken();
            builder.Entity<T>().HasQueryFilter(e => e.TeamId == teamId);
        }
    }
}
