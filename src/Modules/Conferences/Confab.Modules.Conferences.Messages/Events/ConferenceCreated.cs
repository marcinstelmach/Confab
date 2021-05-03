﻿namespace Confab.Modules.Conferences.Messages.Events
{
    using System;
    using Confab.Shared.Abstractions.Events;

    public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTime From, DateTime To) : IEvent;
}