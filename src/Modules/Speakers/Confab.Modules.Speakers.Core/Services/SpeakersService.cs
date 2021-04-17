namespace Confab.Modules.Speakers.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Speakers.Core.Dtos;
    using Confab.Modules.Speakers.Core.Entities;
    using Confab.Modules.Speakers.Core.Exceptions;
    using Confab.Modules.Speakers.Core.Repositories;

    internal class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository _speakersRepository;

        public SpeakersService(ISpeakersRepository speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        public async Task AddAsync(AddSpeakerDto dto)
        {
            var speaker = new Speaker(dto.Name, dto.Age);
            _speakersRepository.Add(speaker);
            await _speakersRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var speaker = await _speakersRepository.Get(id);
            if (speaker is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            _speakersRepository.Delete(speaker);
            await _speakersRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var speaker = await _speakersRepository.Get(id);
            if (speaker is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            return Map(speaker);
        }

        public async Task<IEnumerable<SpeakerDto>> FindAsync()
        {
            return (await _speakersRepository.FindAsync()).Select(Map);
        }

        private static SpeakerDto Map(Speaker speaker)
            => new()
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Age = speaker.Age
            };
    }
}