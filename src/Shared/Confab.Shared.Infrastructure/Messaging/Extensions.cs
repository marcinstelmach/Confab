namespace Confab.Shared.Infrastructure.Messaging
{
    using Abstractions.Messaging;
    using Brokers;
    using Dispatchers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class Extensions
    {
        private const string SectionName = "messaging";
        
        internal static IServiceCollection AddMessaging(this IServiceCollection services)
        {
            services.AddSingleton<IMessageBroker, InMemoryMessageBroker>();
            services.AddSingleton<IMessageChannel, MessageChannel>();
            services.AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>();

            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var messagingOptions = services.GetSettings<MessagingOptions>(SectionName);
            services.Configure<MessagingOptions>(configuration.GetSection(SectionName));
            
            if (messagingOptions.UseBackgroundDispatcher)
            {
                services.AddHostedService<BackgroundDispatcher>();
            }
            
            return services;
        }
    }
}