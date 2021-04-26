namespace Confab.Shared.Infrastructure.Modules
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Confab.Shared.Abstractions.Modules;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class Extensions
    {
        public static IHostBuilder ConfigureModules(this IHostBuilder builder)
            => builder.ConfigureAppConfiguration((ctx, cfg) =>
            {
                foreach (var setting in GetSettings("*"))
                {
                    cfg.AddJsonFile(setting);
                }

                foreach (var setting in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}"))
                {
                    cfg.AddJsonFile(setting);
                }

                IEnumerable<string> GetSettings(string pattern)
                    => Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json",
                        SearchOption.AllDirectories);
            });

        internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IEnumerable<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInto = modules.Select(x => new ModuleInfo(x.Name, x.Path, x.Policies));

            moduleInfoProvider.Modules.AddRange(moduleInto);
            services.AddSingleton(moduleInfoProvider);
            return services;
        }

        internal static void MapModulesInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }
    }
}