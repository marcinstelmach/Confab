using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Conferences.Core.Events
{
    public record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit, DateTimeOffset From, DateTimeOffset To) : IIntegrationEvent;
}