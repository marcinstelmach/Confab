namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    internal interface IConferenceRepository : IRepository
    {
        void Add(Conference conference);

        void Delete(Conference conference);

        Task<Conference> GetAsync(Guid id);

        Task<IEnumerable<Conference>> FindAsync();

    }
}