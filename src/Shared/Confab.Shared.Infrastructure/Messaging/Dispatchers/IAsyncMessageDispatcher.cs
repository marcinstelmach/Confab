namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    using System.Threading.Tasks;
    using Abstractions.Messaging;

    public interface IAsyncMessageDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}