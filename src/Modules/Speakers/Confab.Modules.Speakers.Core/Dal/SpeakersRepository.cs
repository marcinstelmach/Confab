namespace Confab.Modules.Speakers.Core.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Speakers.Core.Entities;
    using Confab.Modules.Speakers.Core.Repositories;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class SpeakersRepository : Repository, ISpeakersRepository
    {
        private readonly SpeakersDbContext _context;

        public SpeakersRepository(SpeakersDbContext context)
            : base(context)
        {
            _context = context;
        }

        public void Add(Speaker speaker)
        {
            _context.Speakers.Add(speaker);
        }

        public void Delete(Speaker speaker)
        {
            _context.Speakers.Remove(speaker);
        }

        public async Task<Speaker> Get(Guid id)
        {
            return await _context.Speakers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Speaker>> FindAsync()
        {
            return await _context.Speakers.ToArrayAsync();
        }
    }
}