namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Abstractions.Messaging;
    using Abstractions.Modules;

    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;

        public InMemoryMessageBroker(IModuleClient moduleClient)
        {
            _moduleClient = moduleClient;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();
            if (!messages.Any())
            {
                return;
            }

            var tasks = messages.Select(message => _moduleClient.PublishAsync(message));
            await Task.WhenAll(tasks);
        }
    }
}