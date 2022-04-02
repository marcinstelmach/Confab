using Confab.Modules.Conferences.Core.Events;

namespace Confab.Modules.Conferences.Core.Entities
{
    using System;
    using Confab.Shared.Abstractions.Events;

    public class Conference  : EventEntity
    {
        public Conference(string name, string location, string logoUrl, int? participantsLimit, DateTimeOffset from, DateTimeOffset to, Host host)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            LogoUrl = logoUrl;
            ParticipantsLimit = participantsLimit;
            From = from;
            To = to;
            Host = host;

            AddEvent(new ConferenceCreated(Id, Name, ParticipantsLimit, From, To));
        }

        private Conference()
        {
        }

        public Guid Id { get; set; }

        public Host Host { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string LogoUrl { get; set; }

        public int? ParticipantsLimit { get; set; }

        public DateTimeOffset From { get; set; }

        public DateTimeOffset To { get; set; }
    }
}