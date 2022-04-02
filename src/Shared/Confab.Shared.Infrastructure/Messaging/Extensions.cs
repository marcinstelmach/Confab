namespace Confab.Shared.Infrastructure.Messaging
{
    using Abstractions.Messaging;
    using Brokers;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            return services;
        }
    }
}