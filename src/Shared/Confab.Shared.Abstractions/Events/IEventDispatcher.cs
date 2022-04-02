namespace Confab.Shared.Abstractions.Events
{
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent eventMessage)
            where TEvent : class, IIntegrationEvent;
        
    }
}