namespace Confab.Modules.Identity.Api
{
    using Confab.Shared.Abstractions.Modules;
    using Confab.Modules.Identity.Core;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public class IdentityModule : IModule
    {
        public string Name => "Identity";

        public string Path => "identity-module";

        public void Load(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}