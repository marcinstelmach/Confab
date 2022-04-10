namespace Confab.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Confab.Shared.Abstractions.Modules;
    using Confab.Shared.Infrastructure;
    using Microsoft.Extensions.Configuration;

    internal static class ModuleLoader
    {
        private const string ModulePart = "Confab.Modules.";

        public static ICollection<Assembly> LoadAssemblies(IConfiguration configuration)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location);
            var modules = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase))
                .ToList();

            var disabledModules = new List<string>();
            foreach (var file in modules)
            {
                if (file.Contains(ModulePart))
                {
                    var moduleName = file.Split(ModulePart)[1].Split(".")[0];
                    var section = configuration.GetSection(moduleName);
                    var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
                    if (!enabled)
                    {
                        disabledModules.Add(file);
                    }
                }
            }

            modules = modules.RemoveMany(disabledModules).ToList();
            foreach (var file in modules)
            {
                assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file)));
            }

            return assemblies;
        }

        public static ICollection<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>()
                .ToArray();
    }
}