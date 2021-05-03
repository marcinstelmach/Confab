namespace Confab.Modules.Tickets.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class TooManyTicketsException : ConfabException
    {
        public TooManyTicketsException(Guid conferenceId)
            : base("Too many tickets would be generated for the conference.")

        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}