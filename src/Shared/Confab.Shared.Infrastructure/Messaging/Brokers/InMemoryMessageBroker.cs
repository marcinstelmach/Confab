using System.Threading.Tasks;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Shared.Infrastructure.Messaging.Brokers
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        public async Task PublishAsync(params IMessage[] messages)
        {
            throw new System.NotImplementedException();
        }
    }
}