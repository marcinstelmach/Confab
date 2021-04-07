namespace Confab.Modules.Conferences.Core.Dtos
{
    using System;
    using System.Collections.Generic;

    public class HostDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ConferenceDto> Conferences { get; set; }
    }
}