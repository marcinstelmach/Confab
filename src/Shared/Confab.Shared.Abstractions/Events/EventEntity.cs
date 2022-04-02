namespace Confab.Shared.Abstractions.Events
{
    using System.Collections.Generic;

    public abstract class EventEntity
    {
        private List<IIntegrationEvent> _events;

        public IReadOnlyCollection<IIntegrationEvent> Events => _events;

        protected void AddEvent(IIntegrationEvent integrationEvent)
        {
            _events ??= new List<IIntegrationEvent>();
            _events.Add(integrationEvent);
        }
    }
}