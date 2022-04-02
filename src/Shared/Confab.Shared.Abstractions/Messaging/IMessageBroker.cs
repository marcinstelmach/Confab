namespace Confab.Shared.Abstractions.Messaging
{
    using System.Threading.Tasks;

    public interface IMessageBroker
    {
        Task PublishAsync(params IMessage[] messages);
    }
}