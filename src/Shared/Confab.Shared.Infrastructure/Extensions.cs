using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.WebApi")]
namespace Confab.Shared.Infrastructure
{
    using Confab.Shared.Abstractions;
    using Confab.Shared.Infrastructure.Api;
    using Confab.Shared.Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddControllers()
                .ConfigureApplicationPartManager(setup =>
                {
                    setup.FeatureProviders.Add(new InternalControllerFeatureProvider());
                });
            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
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