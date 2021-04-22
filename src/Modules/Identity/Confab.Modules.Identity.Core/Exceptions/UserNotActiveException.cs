namespace Confab.Modules.Identity.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class UserNotActiveException : ConfabException
    {
        public UserNotActiveException(Guid id)
            : base($"User with id: '{id}' is not active")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}