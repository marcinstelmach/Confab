namespace Confab.Shared.Abstractions.Auth
{
    using System;
    using System.Collections.Generic;

    public class JsonWebToken
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expires { get; set; }

        public Guid Id { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public IDictionary<string, IEnumerable<string>> Claims { get; set; }
    }
}