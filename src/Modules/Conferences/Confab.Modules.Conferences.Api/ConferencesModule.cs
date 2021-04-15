namespace Confab.Modules.Conferences.Api
{
    using Confab.Modules.Conferences.Core;
    using Confab.Shared.Abstractions.Modules;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    internal class ConferencesModule : IModule
    {
        public string Name => "Conferences";

        public string Path => "conferences-module";

        public void Load(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}