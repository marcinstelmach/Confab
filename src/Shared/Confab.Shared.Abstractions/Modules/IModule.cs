namespace Confab.Shared.Abstractions.Modules
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    public interface IModule
    {
        public string Name { get; }

        public string Path { get; }

        void Load(IServiceCollection services);

        void Use(IApplicationBuilder app);
    }
}