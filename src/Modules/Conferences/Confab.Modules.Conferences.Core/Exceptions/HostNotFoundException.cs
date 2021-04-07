namespace Confab.Modules.Conferences.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class HostNotFoundException : ConfabException
    {
        public HostNotFoundException(Guid id)
            : base($"Host with Id: '{id}' was not found.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}