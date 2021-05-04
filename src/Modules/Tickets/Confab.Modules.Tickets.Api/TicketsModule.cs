namespace Confab.Modules.Tickets.Api
{
    using Confab.Modules.Tickets.Core;
    using Confab.Shared.Abstractions.Modules;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    internal class TicketsModule : IModule
    {
        public string Name => "Tickets";

        public string Path => "tickets-module";

        public void Load(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}