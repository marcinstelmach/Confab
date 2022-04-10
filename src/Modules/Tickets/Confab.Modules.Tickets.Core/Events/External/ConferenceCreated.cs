using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Tickets.Core.Events.External
{
    public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTimeOffset From, DateTimeOffset To) : IEvent;
}