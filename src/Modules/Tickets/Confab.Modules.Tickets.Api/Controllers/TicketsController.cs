namespace Confab.Modules.Tickets.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Services;
    using Confab.Shared.Abstractions.Contexts;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/v1/tickets")]
    internal class TicketsController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;
        private readonly IContext _context;

        public TicketsController(ITicketsService ticketsService, IContext context)
        {
            _ticketsService = ticketsService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var tickets = await _ticketsService.GetForUserAsync(_context.Identity.Id);
            return Ok(tickets);
        }

        [HttpPost("conferences/{conferenceId:guid}/purchase")]
        public async Task<IActionResult> PurchaseAsync(Guid conferenceId)
        {
            await _ticketsService.PurchaseAsync(conferenceId, _context.Identity.Id);
            return NoContent();
        }
    }
}