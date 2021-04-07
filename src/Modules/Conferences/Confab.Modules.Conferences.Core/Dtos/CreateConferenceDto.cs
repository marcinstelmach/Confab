namespace Confab.Modules.Conferences.Core.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateConferenceDto
    {
        [Required]
        public Guid HostId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }


        [StringLength(100, MinimumLength = 30)]
        public string Location { get; set; }

        public string LogoUrl { get; set; }

        public int? ParticipantsLimit { get; set; }

        public DateTimeOffset From { get; set; }

        public DateTimeOffset To { get; set; }
    }
} 