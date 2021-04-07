namespace Confab.Modules.Conferences.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;

    internal class ConferenceRepository : IConferenceRepository
    {
        private readonly List<Conference> _conferences = new List<Conference>();

        public void Add(Conference conference)
        {
            _conferences.Add(conference);
        }

        public void Delete(Conference conference)
        {
            _conferences.Remove(conference);
        }

        public async Task<Conference> GetAsync(Guid id)
        {
            await Task.CompletedTask;
            return _conferences.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Conference>> FindAsync()
        {
            await Task.CompletedTask;
            return _conferences;
        }
    }
}