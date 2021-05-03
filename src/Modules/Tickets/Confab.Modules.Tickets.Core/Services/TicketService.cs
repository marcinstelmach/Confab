namespace Confab.Modules.Tickets.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Tickets.Core.Dtos;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Modules.Tickets.Core.Exceptions;
    using Confab.Modules.Tickets.Core.Repositories;
    using Confab.Shared.Abstractions;

    public class TicketService : ITicketsService
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IConferencesRepository _conferencesRepository;
        private readonly ITicketsRepository _ticketsRepository;
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly ITicketsFactory _ticketsFactory;

        public TicketService(
            IDateTimeService dateTimeService,
            IConferencesRepository conferencesRepository,
            ITicketsRepository ticketsRepository,
            ITicketSaleRepository ticketSaleRepository,
            ITicketsFactory ticketsFactory)
        {
            _dateTimeService = dateTimeService;
            _conferencesRepository = conferencesRepository;
            _ticketsRepository = ticketsRepository;
            _ticketSaleRepository = ticketSaleRepository;
            _ticketsFactory = ticketsFactory;
        }

        public async Task PurchaseAsync(Guid conferenceId, Guid userId)
        {
            var conference = await _conferencesRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(conferenceId);
            }

            var ticket = await _ticketsRepository.GetAsync(conferenceId, userId);
            if (ticket is not null)
            {
                throw new TicketAlreadyPurchasedException(conferenceId, userId);
            }

            var utcNow = _dateTimeService.GetDateTimeUtcNow();
            var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, utcNow);
            if (ticketSale is null)
            {
                throw new TicketSaleUnavailableException(conferenceId);
            }

            if (ticketSale.Amount.HasValue)
            {
                await PurchaseAvailableAsync(ticketSale, userId);
                return;
            }

            ticket = _ticketsFactory.Generate(conferenceId, ticketSale.Id, ticketSale.Price);
            ticket.Purchase(userId, utcNow, ticketSale.Price);
            _ticketsRepository.Add(ticket);
            await _ticketsRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId)
        {
            var tickets = await _ticketsRepository.GetForUserAsync(userId);

            return tickets
                .Select(x => new TicketDto(x.Code, x.Price, x.PurchasedAt.Value,
                    new ConferenceDto(x.ConferenceId, x.Conference.Name))).OrderBy(x => x.PurchasedAt);
        }

        private async Task PurchaseAvailableAsync(TicketSale ticketSale, Guid userId)
        {
            var ticket = ticketSale.Tickets.Where(x => x.UserId is null)
                .OrderBy(_ => Guid.NewGuid())
                .FirstOrDefault();

            if (ticket is null)
            {
                throw new TicketsUnavailableException(ticketSale.ConferenceId);
            }

            ticket.Purchase(userId, _dateTimeService.GetDateTimeUtcNow(), ticketSale.Price);
            await _ticketsRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}