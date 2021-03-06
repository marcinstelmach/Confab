namespace Confab.Modules.Conferences.Core.Dtos
{
    using System;
    using Confab.Modules.Conferences.Core.Entities;

    public class ConferenceDto
    {
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