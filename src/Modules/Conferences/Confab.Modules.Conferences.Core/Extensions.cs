using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core
{
    using Confab.Modules.Conferences.Core.Dal;
    using Confab.Modules.Conferences.Core.Dal.Repositories;
    using Confab.Modules.Conferences.Core.Policies;
    using Confab.Modules.Conferences.Core.Repositories;
    using Confab.Modules.Conferences.Core.Services;
    using Confab.Shared.Infrastructure.MsSql;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSqlDbContext<ConferencesDbContext>();

            services.AddScoped<IHostsRepository, HostsRepository>();
            services.AddScoped<IConferenceRepository, ConferencesRepository>();

            services.AddTransient<IHostDeletionPolicy, HostDeletionPolicy>();
            services.AddTransient<IConferenceDeletionPolicy, ConferenceDeletionPolicy>();
            services.AddTransient<IHostsService, HostsService>();
            services.AddTransient<IConferencesService, ConferencesService>();

            return services;
        }
    }
}