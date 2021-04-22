namespace Confab.Modules.Identity.Core.Dtos
{
    using System;
    using System.Collections.Generic;

    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public Dictionary<string, IEnumerable<string>> Claims { get; set; }

        public DateTimeOffset CreationDateTime { get; set; }
    }
}