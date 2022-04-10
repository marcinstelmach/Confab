namespace Confab.Shared.Abstractions.Events
{
    using System.Threading.Tasks;

    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        Task HandleAsync(TEvent eventMessage);
    }
}