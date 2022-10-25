﻿using Shared.Modules.Layers.Application.CQRS.Command;
using Shared.Modules.Layers.Application.CQRS.DomainEvent;
using Shared.Modules.Layers.Application.CQRS.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Shared.Modules.Layers.Infrastructure.CQRS
{
    public static class CQRSDIRegistrator
    {
        public static IServiceCollection RegisterCQRS(this IServiceCollection services, Assembly assembly)
        {
            Assembly[] assemblies = new Assembly[] { assembly };

            services.TryAddScoped<ICommandDispatcher, CommandDispatcher>();
            services.TryAddScoped<IQueryDispatcher, QueryDispatcher>();
            services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // INFO: Using https://www.nuget.org/packages/Scrutor for registering all Query and Command handlers by convention
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IQueryHandler<,>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(ICommandHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            services.Scan(selector =>
            {
                selector.FromAssemblies(assemblies)
                        .AddClasses(filter =>
                        {
                            filter.AssignableTo(typeof(IDomainEventHandler<>));
                        })
                        .AsImplementedInterfaces()
                        .WithScopedLifetime();
            });
            return services;
        }
    }
}