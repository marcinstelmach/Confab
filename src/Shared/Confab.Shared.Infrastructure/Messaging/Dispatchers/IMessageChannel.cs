namespace Confab.Shared.Infrastructure.Messaging.Dispatchers
{
    using System.Threading.Channels;
    using Abstractions.Messaging;

    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        
        ChannelWriter<IMessage> Writer { get; }
    }
}