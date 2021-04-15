namespace Confab.Modules.Conferences.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class Host
    {
        public Host(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        private Host()
        {
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Conference> Conferences { get; set; }
    }
}