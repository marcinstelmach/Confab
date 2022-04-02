using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleRegistry : IModuleRegistry
    {
        private readonly List<ModuleBroadcastRegistration> _broadcastRegistrations = new();

        public IEnumerable<ModuleBroadcastRegistration> GetBroadcastRegistrations(string key) 
            => _broadcastRegistrations.Where(x => x.Key == key);

        public void AddBroadcastAction(Type requestType, Func<object, Task> func)
        {
            if (string.IsNullOrWhiteSpace(requestType.Namespace))
            {
                throw new InvalidOperationException("Missing namespace.");
            }

            var registration = new ModuleBroadcastRegistration(requestType, func);
            _broadcastRegistrations.Add(registration);
        }
    }
}