namespace Confab.WebApi
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Confab.Shared.Abstractions.Modules;

    internal static class ModuleLoader
    {
        public static IEnumerable<Assembly> LoadAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var locations = assemblies.Where(x => !x.IsDynamic).Select(x => x.Location);
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Where(x => !locations.Contains(x, StringComparer.InvariantCultureIgnoreCase));

            foreach (var file in files)
            {
                assemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file)));
            }

            return assemblies;
        }

        public static IEnumerable<IModule> LoadModules(IEnumerable<Assembly> assemblies)
            => assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(IModule).IsAssignableFrom(x) && !x.IsInterface)
                .OrderBy(x => x.Name)
                .Select(Activator.CreateInstance)
                .Cast<IModule>();
    }
}