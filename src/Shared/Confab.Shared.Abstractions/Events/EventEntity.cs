namespace Confab.Shared.Abstractions.Events
{
    using System.Collections.Generic;

    public abstract class EventEntity
    {
        public List<IEvent> Events { get; private set; }

        public void AddEvent(IEvent @event)
        {
            Events ??= new List<IEvent>();
            Events.Add(@event);
        }
    }
}