using Infrastructure.CQRS.Command;
using Infrastructure.CQRS.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.CQRS
{
    public static class ServiceCollectionExtender
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, Assembly assembly)
        {
            services.TryAddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.TryAddSingleton<IQueryDispatcher, QueryDispatcher>();

            // INFO: Using https://www.nuget.org/packages/Scrutor for registering all Query and Command handlers by convention
            services.Scan(selector =>
            {
                selector.FromAssemblies(assembly)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assembly)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithSingletonLifetime();
            });
            return services;
        }
    }
}
