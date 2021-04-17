namespace Confab.Modules.Speakers.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Speakers.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    internal interface ISpeakersRepository : IRepository
    {
        void Add(Speaker speaker);

        void Delete(Speaker speaker);

        Task<Speaker> Get(Guid id);

        Task<IEnumerable<Speaker>> FindAsync();
    }
}