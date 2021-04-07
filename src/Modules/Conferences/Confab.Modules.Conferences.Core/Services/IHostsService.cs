using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Confab.Modules.Conferences.Api")]
namespace Confab.Modules.Conferences.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Dtos;

    internal interface IHostsService
    {
        Task AddAsync(CreateHostDto dto);

        Task<HostDto> GetAsync(Guid id);

        Task<IEnumerable<HostDto>> FindAsync();

        Task UpdateAsync(HostDto dto);

        Task DeleteAsync(Guid id);
    }
}