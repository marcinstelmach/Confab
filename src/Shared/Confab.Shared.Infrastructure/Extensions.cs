using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.WebApi")]
namespace Confab.Shared.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Confab.Shared.Abstractions;
    using Confab.Shared.Abstractions.Modules;
    using Confab.Shared.Infrastructure.Api;
    using Confab.Shared.Infrastructure.Exceptions;
    using Confab.Shared.Infrastructure.MsSql;
    using Confab.Shared.Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<IModule> enumerable)
        {
            var disabledModules = new List<string>();
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in configuration.AsEnumerable())
            {
                if (key.Contains(":module:enabled", StringComparison.InvariantCultureIgnoreCase) && !bool.Parse(value))
                {
                    disabledModules.Add(key.Split(':')[0]);
                }
            }

            services.AddErrorHandling();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddHostedService<AppInitializer>();
            services.AddControllers()
                .ConfigureApplicationPartManager(setup =>
                {
                    var removedParts = new List<ApplicationPart>();
                    foreach (var disabledModule in disabledModules)
                    {
                        var parts = setup.ApplicationParts.Where(x => x.Name.Contains(disabledModule, StringComparison.InvariantCultureIgnoreCase));
                        removedParts.AddRange(parts);
                    }

                    setup.ApplicationParts.RemoveMany(removedParts);
                    setup.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            services.AddSqlServerConfiguration();
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UserErrorHandling();
            app.UseRouting();
            return app;
        }
    }
}