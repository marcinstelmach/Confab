namespace Confab.Modules.Tickets.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class TicketsUnavailableException : ConfabException
    {
        public TicketsUnavailableException(Guid conferenceId)
            : base("There are no available tickets for the conference.")

        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}