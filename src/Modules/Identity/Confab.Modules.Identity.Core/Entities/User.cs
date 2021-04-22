namespace Confab.Modules.Identity.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public User(string email, string password, string role, Dictionary<string, IEnumerable<string>> claims)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Role = role;
            IsActive = true;
            CreationDateTime = DateTimeOffset.Now;
            Claims = claims;
        }

        private User()
        {
        }

        public Guid Id { get; private set; }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string Role { get; private set; }

        public bool IsActive { get; private set; }

        public DateTimeOffset CreationDateTime { get; private set; }

        public Dictionary<string, IEnumerable<string>> Claims { get; private set; }
    }
}