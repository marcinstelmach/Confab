namespace Confab.Modules.Tickets.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class ConferenceNotFoundException : ConfabNotFoundException
    {
        public ConferenceNotFoundException(Guid id)
            : base($"Conference with Id: '{id}' was not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}