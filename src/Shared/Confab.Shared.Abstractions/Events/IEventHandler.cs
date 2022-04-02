namespace Confab.Shared.Abstractions.Events
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent>
        where TEvent : IIntegrationEvent
    {
        Task HandleAsync(TEvent eventMessage);
    }
}