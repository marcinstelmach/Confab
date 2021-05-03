namespace Confab.Modules.Tickets.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class TicketSaleUnavailableException : ConfabException
    {
        public TicketSaleUnavailableException(Guid conferenceId)
            : base("Ticket sale for the conference is unavailable.")

        {
            ConferenceId = conferenceId;
        }

        public Guid ConferenceId { get; }
    }
}