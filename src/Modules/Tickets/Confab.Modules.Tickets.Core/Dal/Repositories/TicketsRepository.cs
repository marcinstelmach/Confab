namespace Confab.Modules.Tickets.Core.Dal.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Modules.Tickets.Core.Repositories;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class TicketsRepository : Repository, ITicketsRepository
    {
        private readonly TicketsDbContext _context;

        public TicketsRepository(TicketsDbContext context)
            : base(context)
        {
            _context = context;
        }
        public async Task<Ticket> GetAsync(Guid conferenceId, Guid userId)
        {
            return await _context.Tickets.FirstOrDefaultAsync(x => x.ConferenceId == conferenceId && x.UserId == userId);
        }

        public async Task<int> CountForConferenceAsync(Guid conferenceId)
        {
            return await _context.Tickets.CountAsync(x => x.ConferenceId == conferenceId);
        }

        public async Task<IReadOnlyList<Ticket>> GetForUserAsync(Guid userId)
        {
            return await _context.Tickets.AsNoTracking().Where(x => x.UserId == userId).ToArrayAsync();
        }

        public void Add(Ticket ticket)
        {
            _context.Tickets.Add(ticket);
        }

        public void AddMany(IEnumerable<Ticket> tickets)
        {
            _context.Tickets.AddRange(tickets);
        }

        public void Delete(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }
    }
}