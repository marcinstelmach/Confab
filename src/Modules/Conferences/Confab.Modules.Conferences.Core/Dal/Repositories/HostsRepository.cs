namespace Confab.Modules.Conferences.Core.Dal.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Modules.Conferences.Core.Repositories;
    using Confab.Shared.Infrastructure.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class HostsRepository : Repository, IHostsRepository
    {
        private readonly ConferencesDbContext _conferencesDbContext;

        public HostsRepository(ConferencesDbContext conferencesDbContext)
            : base(conferencesDbContext)
        {
            _conferencesDbContext = conferencesDbContext;
        }

        public void Add(Host host)
        {
            _conferencesDbContext.Hosts.Add(host);
        }

        public void Delete(Host host)
        {
            _conferencesDbContext.Hosts.Remove(host);
        }

        public async Task<Host> GetAsync(Guid id)
        {
            return await _conferencesDbContext.Hosts.Include(x => x.Conferences).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Host>> FindAsync()
        {
            return await _conferencesDbContext.Hosts.ToArrayAsync();
        }
    }
}