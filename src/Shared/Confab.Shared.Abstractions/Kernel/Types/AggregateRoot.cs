using System.Collections.Generic;
using System.Linq;

namespace Confab.Shared.Abstractions.Kernel.Types
{
    public abstract class AggregateRoot<TKey>
    {
        private List<IDomainEvent> _events = new();
        private bool _versionIncremented = false;

        public TKey Id { get; protected set; }

        public int Version { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                Version++;
                _versionIncremented = true;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }
            
            Version++;
            _versionIncremented = true;
        }
    }

    public abstract class AggregateRoot : AggregateRoot<AggregateId>
    {
    }
}