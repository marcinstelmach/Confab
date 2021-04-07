namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    internal interface IConferenceRepository
    {
        void Add(Conference conference);

        void Delete(Conference conference);

        Task<Conference> GetAsync(Guid id);

        Task<IEnumerable<Conference>> FindAsync();

    }
}