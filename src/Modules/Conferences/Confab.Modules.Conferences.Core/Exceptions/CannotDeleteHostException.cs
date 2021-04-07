namespace Confab.Modules.Conferences.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    internal class CannotDeleteHostException : ConfabException
    {
        public CannotDeleteHostException(Guid id)
            : base($"Cannot delete host with Id: {id}.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}