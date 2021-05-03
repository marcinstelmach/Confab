namespace Confab.Modules.Tickets.Core.Services
{
    using System;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Shared.Abstractions;

    public class TicketsFactory : ITicketsFactory
    {
        private readonly IDateTimeService _dateTimeService;

        public TicketsFactory(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public Ticket Generate(Guid conferenceId, Guid ticketSaleId, decimal? price)
        {
            return new(conferenceId, ticketSaleId, price, _dateTimeService.GetDateTimeUtcNow());
        }
    }
}