namespace Confab.Shared.Infrastructure.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Confab.Shared.Abstractions.Contexts;

    public class IdentityContext : IIdentityContext
    {
        public IdentityContext(ClaimsPrincipal claimsPrincipal)
        {
            IsAuthenticated = claimsPrincipal.Identity?.IsAuthenticated is true;
            Id = IsAuthenticated ? Guid.Parse(claimsPrincipal.Identity.Name) : Guid.Empty;
            Role = IsAuthenticated ? claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.Role)?.Value : string.Empty;
            Claims = IsAuthenticated
                ? claimsPrincipal.Claims.GroupBy(x => x.Type)
                    .ToDictionary(x => x.Key, x => x.Select(u => u.Value.ToString()))
                : new Dictionary<string, IEnumerable<string>>();
        }

        public bool IsAuthenticated { get; }
        public Guid Id { get; }
        public string Role { get; }
        public Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}