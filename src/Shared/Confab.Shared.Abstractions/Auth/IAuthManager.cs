namespace Confab.Shared.Abstractions.Auth
{
    using System;
    using System.Collections.Generic;

    public interface IAuthManager
    {
        JsonWebToken GenerateToken(Guid userId, string email,string role, IDictionary<string, IEnumerable<string>> claims);
    }
}