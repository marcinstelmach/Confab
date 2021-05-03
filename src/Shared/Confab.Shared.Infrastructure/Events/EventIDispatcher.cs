namespace Confab.Shared.Infrastructure.Events
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Shared.Abstractions.Events;
    using Microsoft.Extensions.DependencyInjection;

    internal class EventIDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventIDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TEvent>(TEvent eventMessage)
            where TEvent : class, IEvent
        {
            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();
            var tasks = handlers.Select(x => x.HandleAsync(eventMessage));
            await Task.WhenAll(tasks);
        }
    }
}