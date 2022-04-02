using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Shared.Abstractions.Events;
using Confab.Shared.Abstractions.Modules;
using Microsoft.EntityFrameworkCore;

namespace Confab.Shared.Infrastructure.Modules
{
    internal sealed class ModuleClient : IModuleClient
    {
        private readonly IModuleRegistry _moduleRegistry;
        private readonly IModuleSerializer _moduleSerializer;

        public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
        {
            _moduleRegistry = moduleRegistry;
            _moduleSerializer = moduleSerializer;
        }

        public async Task PublishAsync(object message)
        {
            var key = message.GetType().Name;
            var registrations = _moduleRegistry.GetBroadcastRegistrations(key);

            var tasks = new List<Task>();
            foreach (var registration in registrations)
            {
                var receiverMessage = TranslateType(message, registration.ReceiverType);
                tasks.Add(registration.Action(receiverMessage));
            }

            await Task.WhenAll(tasks);
        }

        public async Task PublishAsync(DbContext context)
        {
            var events = context.ChangeTracker
                .Entries<EventEntity>()
                .Where(x => x.Entity.Events is not null && x.Entity.Events.Any())
                .SelectMany(x => x.Entity.Events);
            foreach (var @event in events)
            {
                await PublishAsync(@event);
            }
        }

        private object TranslateType(object value, Type type)
        {
            if (value.GetType() == type)
            {
                return value;
            }
            
            return _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
        }
    }
}