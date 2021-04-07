namespace Confab.Shared.Abstractions
{
    using System;

    public interface IDateTimeService
    {
        DateTimeOffset GetUtcNow();
    }
}