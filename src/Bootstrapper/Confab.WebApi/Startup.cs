namespace Confab.WebApi
{
    using System.Collections.Generic;
    using System.Reflection;
    using Confab.Shared.Abstractions.Modules;
    using Confab.Shared.Infrastructure;
    using Confab.Shared.Infrastructure.Modules;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly ICollection<Assembly> _assemblies;
        private readonly ICollection<IModule> _modules;

        public Startup(IConfiguration configuration)
        {
            _assemblies = ModuleLoader.LoadAssemblies(configuration);
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(_assemblies, _modules);
            foreach (var module in _modules)
            {
                module.Load(services);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseInfrastructure();
            foreach (var module in _modules)
            {
                module.Use(app);
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async x => await x.Response.WriteAsync("Hello"));
                endpoints.MapModulesInfo();
            });
        }
    }
}
