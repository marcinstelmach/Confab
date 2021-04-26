namespace Confab.Shared.Abstractions.Contexts
{
    using System;
    using System.Collections.Generic;

    public interface IIdentityContext
    {
        public bool IsAuthenticated { get; }

        public Guid Id { get; }

        public string Role { get; }

        public Dictionary<string, IEnumerable<string>> Claims { get; }
    }
}