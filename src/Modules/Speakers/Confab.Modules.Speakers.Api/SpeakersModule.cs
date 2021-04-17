namespace Confab.Modules.Speakers.Api
{
    using Confab.Modules.Speakers.Core;
    using Confab.Shared.Abstractions.Modules;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    internal class SpeakersModule : IModule
    {
        public string Name => "Speakers";

        public string Path => "speakers-module";

        public void Load(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}