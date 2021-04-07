namespace Confab.Modules.Conferences.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Dtos;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Modules.Conferences.Core.Exceptions;
    using Confab.Modules.Conferences.Core.Policies;
    using Confab.Modules.Conferences.Core.Repositories;

    internal class ConferencesService : IConferencesService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostsRepository _hostsRepository;
        private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;

        public ConferencesService(IConferenceRepository conferenceRepository, IHostsRepository hostsRepository, IConferenceDeletionPolicy conferenceDeletionPolicy)
        {
            _conferenceRepository = conferenceRepository;
            _hostsRepository = hostsRepository;
            _conferenceDeletionPolicy = conferenceDeletionPolicy;
        }

        public async Task AddAsync(CreateConferenceDto dto)
        {
            var host = await _hostsRepository.GetAsync(dto.HostId);
            if (host is null)
            {
                throw new HostNotFoundException(dto.HostId);
            }

            var conference = new Conference(dto.Name, dto.Location, dto.LogoUrl, dto.ParticipantsLimit, dto.From, dto.To, host);
            _conferenceRepository.Add(conference);
        }

        public async Task<ConferenceDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            return conference is null
                ? null
                : Map(conference);
        }

        public async Task<IEnumerable<ConferenceDto>> FindAsync()
        {
            var conferences = await _conferenceRepository.FindAsync();
            return conferences.Select(Map);
        }

        public async Task UpdateAsync(ConferenceDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.Id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            // update properties
        }

        public async Task DeleteAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            if (!await _conferenceDeletionPolicy.CanDeleteAsync(conference))
            {
                throw new CannotDeleteConferenceException(id);
            }

            _conferenceRepository.Delete(conference);
        }

        private static ConferenceDto Map(Conference conference)
        {
            return new ConferenceDto
            {
                Id = conference.Id,
                Name = conference.Name,
                Location = conference.Location,
                LogoUrl = conference.LogoUrl,
                ParticipantsLimit = conference.ParticipantsLimit,
                From = conference.From,
                To = conference.To,
                Host = conference.Host
            };
        }

    }
}