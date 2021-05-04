namespace Confab.Shared.Abstractions.Events
{
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public interface IEventDispatcher
    {
        Task DispatchAsync<TEvent>(TEvent eventMessage)
            where TEvent : class, IEvent;

        Task DispatchAsync(DbContext context);
    }
}