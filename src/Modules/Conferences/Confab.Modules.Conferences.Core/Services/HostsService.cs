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

    internal class HostsService : IHostsService
    {
        private readonly IHostsRepository _hostsRepository;
        private readonly IHostDeletionPolicy _hostDeletionPolicy;

        public HostsService(IHostsRepository hostsRepository, IHostDeletionPolicy hostDeletionPolicy)
        {
            _hostsRepository = hostsRepository;
            _hostDeletionPolicy = hostDeletionPolicy;
        }

        public async Task AddAsync(CreateHostDto dto)
        {
            await Task.CompletedTask;
            var host = new Host(dto.Name, dto.Description);
            _hostsRepository.Add(host);
        }

        public async Task<HostDto> GetAsync(Guid id)
        {
            var host = await _hostsRepository.GetAsync(id);
            return host is null
                ? null
                : Map(host);
        }

        public async Task<IEnumerable<HostDto>> FindAsync()
        {
            var hosts = await _hostsRepository.FindAsync();
            return hosts.Select(Map);
        }

        public async Task UpdateAsync(HostDto dto)
        {
            var host = await _hostsRepository.GetAsync(dto.Id);
            if (host is null)
            {
                throw new HostNotFoundException(dto.Id);
            }

            host.Name = dto.Name;
            host.Description = dto.Description;
        }

        public async Task DeleteAsync(Guid id)
        {
            var host = await _hostsRepository.GetAsync(id);
            if (host is null)
            {
                throw new HostNotFoundException(id);
            }

            if (!await _hostDeletionPolicy.CanDeleteAsync(host))
            {
                throw new CannotDeleteHostException(id);
            }

            _hostsRepository.Delete(host);
        }

        private static HostDto Map(Host host) => new HostDto
        {
            Id = host.Id,
            Name = host.Name,
            Description = host.Description,
            Conferences = host.Conferences.Select(x => new ConferenceDto
            {
                Id = x.Id,
                Host = x.Host,
                Location = x.Location,
                LogoUrl = x.LogoUrl,
                ParticipantsLimit = x.ParticipantsLimit,
                Name = x.Name,
                From = x.From,
                To = x.To
            })
        };
    }
}