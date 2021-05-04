using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Speakers.Api")]
namespace Confab.Modules.Speakers.Core
{
    using Confab.Modules.Speakers.Core.Dal;
    using Confab.Modules.Speakers.Core.Repositories;
    using Confab.Modules.Speakers.Core.Services;
    using Confab.Shared.Infrastructure.MsSql;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSqlDbContext<SpeakersDbContext>();
            services.AddScoped<ISpeakersRepository, SpeakersRepository>();
            services.AddTransient<ISpeakersService, SpeakersService>();
            return services;
        }
    }
}