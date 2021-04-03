using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.WebApi")]
namespace Confab.Modules.Conferences.Api
{
    using Confab.Modules.Conferences.Core;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddConferences(this IServiceCollection services)
        {
            services.AddCore();
            return services;
        }
    }
}