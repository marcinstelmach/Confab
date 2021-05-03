namespace Confab.Modules.Tickets.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;

    public interface ITicketsRepository : IRepository
    {
        Task<Ticket> GetAsync(Guid conferenceId, Guid userId);

        Task<int> CountForConferenceAsync(Guid conferenceId);

        Task<IReadOnlyList<Ticket>> GetForUserAsync(Guid userId);

        void Add(Ticket ticket);

        void AddMany(IEnumerable<Ticket> tickets);

        void Delete(Ticket ticket);
    }
}