namespace Confab.Modules.Tickets.Core.Dal.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Modules.Tickets.Core.Repositories;using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class TicketSaleRepository : Repository, ITicketSaleRepository
    {
        private readonly TicketsDbContext _context;

        public TicketSaleRepository(TicketsDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<TicketSale> GetAsync(Guid id)
        {
            return await _context.TicketSales.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TicketSale> GetCurrentForConferenceAsync(Guid conferenceId, DateTime now)
        {
            return await _context.TicketSales
                .Where(x => x.ConferenceId == conferenceId)
                .OrderBy(x => x.From)
                .Include(x => x.Tickets)
                .LastOrDefaultAsync(x => x.From <= now && x.To >= now);
        }

        public async Task<IReadOnlyList<TicketSale>> BrowseForConferenceAsync(Guid conferenceId)
        {
            return await _context.TicketSales
                .AsNoTracking()
                .Where(x => x.ConferenceId == conferenceId)
                .Include(x => x.Tickets)
                .AsNoTrackingWithIdentityResolution()
                .ToArrayAsync();
        }

        public void Add(TicketSale ticketSale)
        {
            _context.TicketSales.Add(ticketSale);
        }

        public void Delete(TicketSale ticketSale)
        {
            _context.TicketSales.Remove(ticketSale);
        }
    }
}