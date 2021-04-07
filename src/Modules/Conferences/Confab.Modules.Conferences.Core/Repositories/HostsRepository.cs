namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    internal class HostsRepository : IHostsRepository
    {
        private readonly List<Host> _hosts = new List<Host>();

        public void Add(Host host)
        {
            _hosts.Add(host);
        }

        public void Delete(Host host)
        {
            _hosts.Remove(host);
        }

        public async Task<Host> GetAsync(Guid id)
        {
            await Task.CompletedTask;
            return _hosts.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Host>> FindAsync()
        {
            await Task.CompletedTask;
            return _hosts;
        }
    }
}