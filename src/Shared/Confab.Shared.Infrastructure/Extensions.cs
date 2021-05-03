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
    using Confab.Shared.Infrastructure.Auth;
    using Confab.Shared.Infrastructure.Contexts;
    using Confab.Shared.Infrastructure.Events;
    using Confab.Shared.Infrastructure.Exceptions;
    using Confab.Shared.Infrastructure.Modules;
    using Confab.Shared.Infrastructure.MsSql;
    using Confab.Shared.Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.ApplicationParts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    internal static class Extensions
    {
        private const string CorsPolicy = "cors";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IEnumerable<Assembly> assemblies, ICollection<IModule> modules)
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

            services.AddCors(cors =>
            {
                cors.AddPolicy(CorsPolicy, x =>
                {
                    x.WithOrigins("*")
                        .WithMethods("POST", "PUT", "DELETE")
                        .WithHeaders("Content-Type", "Authorization");
                });
            });

            services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(y => y.FullName);
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Confab API",
                    Version = "v1"
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IContextFactory, ContextFactory>();
            services.AddTransient(x => x.GetRequiredService<IContextFactory>().Create());
            services.AddModuleInfo(modules);
            services.AddErrorHandling();
            services.AddEvents(assemblies);
            services.AddSingleton<IDateTimeService, DateTimeService>();
            ////services.AddHostedService<AppInitializer>();
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
            services.AddAuth(modules);
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicy);
            app.UserErrorHandling();
            app.UseSwagger();
            ////app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Confab"));
            app.UseReDoc(x =>
            {
                x.RoutePrefix = "docs";
                x.SpecUrl("/swagger/v1/swagger.json");
                x.DocumentTitle = "Confab API";
            });
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            return app;
        }

        public static TSettings GetSettings<TSettings>(this IConfiguration configuration, string key = null)
            where TSettings : new()
        {
            TSettings settings = new();
            configuration.GetSection(key ?? typeof(TSettings).Name).Bind(settings);
            return settings;
        }
    }
}