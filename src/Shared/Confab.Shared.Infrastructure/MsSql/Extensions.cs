namespace Confab.Shared.Infrastructure.MsSql
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static class Extensions
    {
        internal static IServiceCollection AddSqlServerConfiguration(this IServiceCollection services)
        {
            using var provider = services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            services.Configure<MsSqlSettings>(configuration.GetSection("MsSqlSettings"));

            return services;
        }

        public static IServiceCollection AddDbContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            using var provider = services.BuildServiceProvider();
            var mssqlOptions = provider.GetRequiredService<IOptions<MsSqlSettings>>();
            services.AddDbContext<T>(x => x.UseSqlServer(mssqlOptions.Value.ConnectionString));
            return services;
        }
    }
}