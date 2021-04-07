namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    internal interface IHostsRepository
    {
        void Add(Host host);

        void Delete(Host host);

        Task<Host> GetAsync(Guid id);

        Task<IEnumerable<Host>> FindAsync();
    }
}