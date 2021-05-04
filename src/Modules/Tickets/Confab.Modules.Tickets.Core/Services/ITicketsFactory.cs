namespace Confab.Modules.Tickets.Core.Services
{
    using System;
    using Confab.Modules.Tickets.Core.Entities;

    public interface ITicketsFactory
    {
        Ticket Generate(Guid conferenceId, decimal? price, TicketSale ticketSale);
    }
}