using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.WebApi")]
namespace Confab.Shared.Infrastructure
{
    using Confab.Shared.Abstractions;
    using Confab.Shared.Infrastructure.Api;
    using Confab.Shared.Infrastructure.Exceptions;
    using Confab.Shared.Infrastructure.MsSql;
    using Confab.Shared.Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddErrorHandling();
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddHostedService<AppInitializer>();
            services.AddControllers()
                .ConfigureApplicationPartManager(setup =>
                {
                    setup.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            services.AddSqlServerConfiguration();
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UserErrorHandling();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async x => await x.Response.WriteAsync("Hello"));
            });
            return app;
        }
    }
}