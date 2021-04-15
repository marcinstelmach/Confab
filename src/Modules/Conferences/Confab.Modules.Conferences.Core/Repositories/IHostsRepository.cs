namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    internal interface IHostsRepository : IRepository
    {
        void Add(Host host);

        void Delete(Host host);

        Task<Host> GetAsync(Guid id);

        Task<IEnumerable<Host>> FindAsync();
    }
}