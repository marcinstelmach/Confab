using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Identity.Api")]
namespace Confab.Modules.Identity.Core
{
    using Confab.Modules.Identity.Core.Dal;
    using Confab.Modules.Identity.Core.Entities;
    using Confab.Modules.Identity.Core.Services;
    using Confab.Shared.Infrastructure.MsSql;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSqlDbContext<IdentityDbContext>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            return services;
        }
    }
}