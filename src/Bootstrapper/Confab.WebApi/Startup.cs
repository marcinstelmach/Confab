namespace Confab.WebApi
{
    using System.Collections.Generic;
    using System.Reflection;
    using Confab.Shared.Abstractions.Modules;
    using Confab.Shared.Infrastructure;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly IEnumerable<IModule> _modules;

        public Startup()
        {
            _assemblies = ModuleLoader.LoadAssemblies();
            _modules = ModuleLoader.LoadModules(_assemblies);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure();
            foreach (var module in _modules)
            {
                module.Load(services);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            });
        }
    }
}
