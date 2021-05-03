﻿namespace Confab.Modules.Tickets.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class TicketAlreadyPurchasedException : ConfabException
    {
        public TicketAlreadyPurchasedException(Guid conferenceId, Guid userId)
            : base("Ticket for the conference has been already purchased.")

        {
            ConferenceId = conferenceId;
            UserId = userId;
        }

        public Guid ConferenceId { get; }

        public Guid UserId { get; }
    }
}