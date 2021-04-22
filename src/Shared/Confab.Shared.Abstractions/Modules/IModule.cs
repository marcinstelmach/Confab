namespace Confab.Shared.Abstractions.Modules
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public interface IModule
    {
        public string Name { get; }

        public string Path { get; }

        public IEnumerable<string> Policies => Enumerable.Empty<string>();

        void Load(IServiceCollection services);

        void Use(IApplicationBuilder app);
    }
}