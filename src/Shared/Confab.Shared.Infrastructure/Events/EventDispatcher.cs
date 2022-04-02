namespace Confab.Shared.Infrastructure.Events
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Confab.Shared.Abstractions.Events;
    using Microsoft.Extensions.DependencyInjection;

    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task PublishAsync<TEvent>(TEvent eventMessage)
            where TEvent : class, IIntegrationEvent
        {
            using var scope = _serviceProvider.CreateScope();
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventMessage.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);
            var method = handlerType.GetMethod(nameof(IEventHandler<TEvent>.HandleAsync));

            var tasks = handlers.Select(handler => (Task)method.Invoke(handler, new object[] { eventMessage }));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}