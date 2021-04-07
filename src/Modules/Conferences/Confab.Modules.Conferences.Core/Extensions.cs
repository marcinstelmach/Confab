using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    using Confab.Modules.Conferences.Core.Policies;
    using Confab.Modules.Conferences.Core.Repositories;
    using Confab.Modules.Conferences.Core.Services;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IHostsRepository, HostsRepository>();
            services.AddTransient<IHostDeletionPolicy, HostDeletionPolicy>();
            services.AddTransient<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
            services.AddTransient<IHostsService, HostsService>();

            return services;
        }
    }
}