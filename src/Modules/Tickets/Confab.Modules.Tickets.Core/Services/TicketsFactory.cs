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

        public Ticket Generate(Guid conferenceId, decimal? price, TicketSale ticketSale)
        {
            return new(conferenceId, price, ticketSale, _dateTimeService.GetDateTimeUtcNow());
        }
    }
}