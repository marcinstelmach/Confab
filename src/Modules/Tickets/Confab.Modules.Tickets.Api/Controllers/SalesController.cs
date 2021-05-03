namespace Confab.Modules.Tickets.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Dtos;
    using Confab.Modules.Tickets.Core.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/sales")]
    internal class SalesController : ControllerBase
    {
        private const string Policy = "tickets";
        private readonly ITicketSalesService _ticketSaleService;

        public SalesController(ITicketSalesService ticketSaleService)
        {
            _ticketSaleService = ticketSaleService;
        }

        [HttpGet("conferences/{conferenceId:guid}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<TicketSaleInfoDto>>> GetAll(Guid conferenceId)
        {
            var tickets = await _ticketSaleService.GetAllAsync(conferenceId);
            if (tickets is null)
            {
                return NotFound();
            }

            return Ok(tickets);
        }

        [HttpGet("conferences/{conferenceId:guid}/current")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TicketSaleInfoDto>> GetCurrent(Guid conferenceId)
        {
            var ticket = await _ticketSaleService.GetCurrentAsync(conferenceId);
            if (ticket is null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [Authorize(Policy)]
        [HttpPost("conferences/{conferenceId:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<ActionResult> AddTickets(Guid conferenceId, TicketSaleDto dto)
        {
            dto.ConferenceId = conferenceId;
            await _ticketSaleService.AddAsync(dto);
            return NoContent();
        }
    }
}