namespace Confab.Modules.Speakers.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Speakers.Core.Dtos;

    internal interface ISpeakersService
    {
        Task AddAsync(AddSpeakerDto dto);

        Task DeleteAsync(Guid id);

        Task<SpeakerDto> GetAsync(Guid id);

        Task<IEnumerable<SpeakerDto>> FindAsync();
    }
}