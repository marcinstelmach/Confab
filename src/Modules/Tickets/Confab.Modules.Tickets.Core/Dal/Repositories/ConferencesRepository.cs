namespace Confab.Modules.Tickets.Core.Dal.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Modules.Tickets.Core.Repositories;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class ConferencesRepository : Repository, IConferencesRepository
    {
        private readonly TicketsDbContext _context;

        public ConferencesRepository(TicketsDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Conference> GetAsync(Guid id)
        {
            return await _context.Conferences.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Add(Conference conference)
        {
            _context.Conferences.Add(conference);
        }

        public void Delete(Conference conference)
        {
            _context.Conferences.Remove(conference);
        }
    }
}