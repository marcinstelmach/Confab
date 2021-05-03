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

    public class TicketSalesService : ITicketSalesService
    {
        private readonly IConferencesRepository _conferenceRepository;
        private readonly ITicketSaleRepository _ticketSaleRepository;
        private readonly ITicketsRepository _ticketRepository;
        private readonly ITicketsFactory _ticketsFactory;
        private readonly IDateTimeService _dateTimeService;

        public TicketSalesService(
            IConferencesRepository conferenceRepository,
            ITicketSaleRepository ticketSaleRepository,
            ITicketsRepository ticketRepository,
            ITicketsFactory ticketsFactory,
            IDateTimeService dateTimeService)
        {
            _conferenceRepository = conferenceRepository;
            _ticketSaleRepository = ticketSaleRepository;
            _ticketRepository = ticketRepository;
            _ticketsFactory = ticketsFactory;
            _dateTimeService = dateTimeService;
        }

        public async Task AddAsync(TicketSaleDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.ConferenceId);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.ConferenceId);
            }

            if (conference.ParticipantsLimit.HasValue)
            {
                var ticketsCount = await _ticketRepository.CountForConferenceAsync(conference.Id);
                if (ticketsCount + dto.Amount > conference.ParticipantsLimit)
                {
                    throw new TooManyTicketsException(conference.Id);
                }
            }

            var ticketSale = new TicketSale(dto.ConferenceId, dto.Name, dto.Price, dto.Amount, dto.From, dto.To);
            _ticketSaleRepository.Add(ticketSale);
            await _ticketSaleRepository.UnitOfWork.SaveChangesAsync();

            if (ticketSale.Amount.HasValue)
            {
                var tickets = new List<Ticket>();
                for (var i = 0; i < ticketSale.Amount; i++)
                {
                    var ticket = _ticketsFactory.Generate(conference.Id, ticketSale.Id, ticketSale.Price);
                    tickets.Add(ticket);
                }

                _ticketRepository.AddMany(tickets);
                await _ticketRepository.UnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TicketSaleInfoDto>> GetAllAsync(Guid conferenceId)
        {
            var conference = await _conferenceRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                return null;
            }

            var ticketSales = await _ticketSaleRepository.BrowseForConferenceAsync(conferenceId);
            return ticketSales.Select(x => Map(x, conference));
        }

        public async Task<TicketSaleInfoDto> GetCurrentAsync(Guid conferenceId)
        {
            var conference = await _conferenceRepository.GetAsync(conferenceId);
            if (conference is null)
            {
                return null;
            }

            var now = _dateTimeService.GetDateTimeUtcNow();
            var ticketSale = await _ticketSaleRepository.GetCurrentForConferenceAsync(conferenceId, now);
            return ticketSale is not null ? Map(ticketSale, conference) : null;
        }

        private static TicketSaleInfoDto Map(TicketSale ticketSale, Conference conference)
        {
            int? availableTickets = null;
            var totalTickets = ticketSale.Amount;
            if (totalTickets.HasValue)
            {
                availableTickets = ticketSale.Tickets.Count(x => x.UserId is null);
            }

            return new TicketSaleInfoDto(ticketSale.Name, new ConferenceDto(conference.Id, conference.Name), ticketSale.Price,
                totalTickets, availableTickets, ticketSale.From, ticketSale.To);
        }
    }
}