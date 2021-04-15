namespace Confab.Modules.Conferences.Core.Dal.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Modules.Conferences.Core.Repositories;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class ConferencesRepository : Repository, IConferenceRepository
    {
        private readonly ConferencesDbContext _context;

        public ConferencesRepository(ConferencesDbContext context)
            : base(context)
        {
            _context = context;
        }

        public void Add(Conference conference)
        {
            _context.Conferences.Add(conference);
        }

        public void Delete(Conference conference)
        {
            _context.Conferences.Remove(conference);
        }

        public async Task<Conference> GetAsync(Guid id)
        {
            return await _context.Conferences.Include(x => x.Host).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Conference>> FindAsync()
        {
            return await _context.Conferences.ToArrayAsync();
        }
    }
}