using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Tickets.Api")]
namespace Confab.Modules.Tickets.Core
{
    using Confab.Modules.Tickets.Core.Dal;
    using Confab.Modules.Tickets.Core.Dal.Repositories;
    using Confab.Modules.Tickets.Core.Repositories;
    using Confab.Modules.Tickets.Core.Services;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
            => services
                .AddScoped<ITicketsService, TicketService>()
                .AddScoped<ITicketSalesService, TicketSalesService>()
                .AddScoped<IConferencesRepository, ConferencesRepository>()
                .AddScoped<ITicketsRepository, TicketsRepository>()
                .AddScoped<ITicketSaleRepository, TicketSaleRepository>()
                .AddSingleton<ITicketsFactory, TicketsFactory>()
                .AddDbContext<TicketsDbContext>();
    }
}