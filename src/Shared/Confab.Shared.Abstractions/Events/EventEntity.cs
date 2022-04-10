namespace Confab.Shared.Abstractions.Events
{
    using System.Collections.Generic;

    public abstract class EventEntity
    {
        private List<IEvent> _events;

        public IReadOnlyCollection<IEvent> Events => _events;

        protected void AddEvent(IEvent @event)
        {
            _events ??= new List<IEvent>();
            _events.Add(@event);
        }
    }
}