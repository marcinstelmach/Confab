namespace Confab.Modules.Tickets.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    public interface ITicketSaleRepository : IRepository
    {
        Task<TicketSale> GetAsync(Guid id);

        Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now);

        Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId);

        void Add(TicketSale ticketSale);

        void Delete(TicketSale ticketSale);
    }
}