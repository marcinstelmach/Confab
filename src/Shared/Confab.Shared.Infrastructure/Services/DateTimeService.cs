namespace Confab.Shared.Infrastructure.Services
{
    using System;
    using Confab.Shared.Abstractions;

    public class DateTimeService : IDateTimeService
    {
        public DateTimeOffset GetUtcNow()
        {
            return DateTimeOffset.UtcNow;
        }

        public DateTime GetDateTimeUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}