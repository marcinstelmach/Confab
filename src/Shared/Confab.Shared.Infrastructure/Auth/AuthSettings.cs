﻿namespace Confab.Shared.Infrastructure.Auth
{
    public class AuthSettings
    {
        public string SecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int AccessTokenLifetimeMinutes { get; set; }
    }
}