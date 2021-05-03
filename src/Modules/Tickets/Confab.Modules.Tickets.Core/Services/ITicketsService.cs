namespace Confab.Modules.Tickets.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Dtos;

    public interface ITicketsService
    {
        Task PurchaseAsync(Guid conferenceId, Guid userId);

        Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId);
    }
}