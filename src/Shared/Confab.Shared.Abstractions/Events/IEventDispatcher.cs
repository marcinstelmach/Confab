namespace Confab.Shared.Abstractions.Events
{
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent eventMessage)
            where TEvent : class, IEvent;
    }
}