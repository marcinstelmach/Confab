using System;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public class Speaker : AggregateRoot
    {
        private Speaker(AggregateId id, string fullName)
        {
            Id = id;
            FullName = fullName;
        }
        
        public string FullName { get; init; }

        public static Speaker Create(Guid id, string fullName) => new Speaker(id, fullName);
    }
}