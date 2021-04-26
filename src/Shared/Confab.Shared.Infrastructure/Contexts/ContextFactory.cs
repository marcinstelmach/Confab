namespace Confab.Shared.Infrastructure.Contexts
{
    using Confab.Shared.Abstractions.Contexts;
    using Microsoft.AspNetCore.Http;

    public class ContextFactory : IContextFactory
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IContext Create()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            return httpContext is null
                ? Context.Empty()
                : new Context(httpContext);
        }
    }
}