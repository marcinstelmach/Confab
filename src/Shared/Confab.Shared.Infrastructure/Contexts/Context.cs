namespace Confab.Shared.Infrastructure.Contexts
{
    using System;
    using Confab.Shared.Abstractions.Contexts;
    using Microsoft.AspNetCore.Http;

    internal class Context : IContext
    {
        internal Context(HttpContext httpContext)
            : this(httpContext.TraceIdentifier, new IdentityContext(httpContext.User))
        {
        }

        internal Context(string traceId, IIdentityContext identity)
        {
            TraceId = traceId;
            Identity = identity;
        }

        private Context()
        {
        }

        public string RequestId => $"{Guid.NewGuid():N}";

        public string TraceId { get; }

        public IIdentityContext Identity { get; }

        public static IContext Empty() => new Context();
    }
}