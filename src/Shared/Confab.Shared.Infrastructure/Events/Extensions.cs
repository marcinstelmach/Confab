namespace Confab.Shared.Infrastructure.Events
{
    using System.Collections.Generic;
    using System.Reflection;
    using Confab.Shared.Abstractions.Events;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.Scan(x => x.FromAssemblies(assemblies)
                .AddClasses(y => y.AssignableTo(typeof(IEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}