namespace Confab.Modules.Tickets.Core.Events.External.Handlers
{
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Messages.Events;
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Modules.Tickets.Core.Repositories;
    using Confab.Shared.Abstractions.Events;

    public class ConferenceCreatedHandler : IEventHandler<ConferenceCreated>
    {
        private readonly IConferencesRepository _conferencesRepository;

        public ConferenceCreatedHandler(IConferencesRepository conferencesRepository)
        {
            _conferencesRepository = conferencesRepository;
        }

        public async Task HandleAsync(ConferenceCreated eventMessage)
        {
            var conference = new Conference(eventMessage.Id, eventMessage.Name, eventMessage.ParticipantsLimit, eventMessage.From, eventMessage.To);

            _conferencesRepository.Add(conference);
            await _conferencesRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}