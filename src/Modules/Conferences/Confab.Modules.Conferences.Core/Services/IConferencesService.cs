using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Dtos;

    internal interface IConferencesService
    {
        Task AddAsync(CreateConferenceDto dto);

        Task<ConferenceDto> GetAsync(Guid id);

        Task<IEnumerable<ConferenceDto>> FindAsync();

        Task UpdateAsync(ConferenceDto dto);

        Task DeleteAsync(Guid id);
    }
}