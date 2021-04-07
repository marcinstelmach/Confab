namespace Confab.Modules.Conferences.Core.Entities
{
    using System;

    public class Conference
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