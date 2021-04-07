namespace Confab.Modules.Conferences.Core.Policies
{
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions;

    internal class ConferenceDeletionPolicy : IConferenceDeletionPolicy
    {
        private readonly IDateTimeService _dateTimeService;

        public ConferenceDeletionPolicy(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public async Task<bool> CanDeleteAsync(Conference conference)
        {
            await Task.CompletedTask;

            // TODO: Check if there are any participants
            return _dateTimeService.GetUtcNow().Date.AddDays(7) < conference.From.Date;
        }
    }
}