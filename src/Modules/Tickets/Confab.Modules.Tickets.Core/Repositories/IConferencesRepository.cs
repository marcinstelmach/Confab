namespace Confab.Modules.Tickets.Core.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    public interface IConferencesRepository : IRepository
    {
        Task<Conference> GetAsync(Guid id);

        void Add(Conference conference);

        void Delete(Conference conference);
    }
}