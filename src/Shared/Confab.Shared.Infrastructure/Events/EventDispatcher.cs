namespace Confab.Shared.Infrastructure.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Shared.Abstractions.Events;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task DispatchAsync<TEvent>(TEvent eventMessage) where TEvent : class, IEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventMessage.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);
            var method = handlerType.GetMethod(nameof(IEventHandler<TEvent>.HandleAsync));

            var tasks = handlers.Select(handler => (Task)method.Invoke(handler, new object[] { eventMessage }));
            await Task.WhenAll(tasks).ConfigureAwait(false);

        }

        public async Task DispatchAsync(DbContext context)
        {
            var events = context.ChangeTracker
                .Entries<EventEntity>()
                .Where(x => x.Entity.Events is not null && x.Entity.Events.Any())
                .SelectMany(x => x.Entity.Events);

            foreach (var @event in events)
            {
                await DispatchAsync(@event);
            }
        }
    }
}