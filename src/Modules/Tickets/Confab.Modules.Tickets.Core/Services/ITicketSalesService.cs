namespace Confab.Modules.Tickets.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Dtos;

    public interface ITicketSalesService
    {
        Task AddAsync(TicketSaleDto dto);

        Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId);

        Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId);
    }
}