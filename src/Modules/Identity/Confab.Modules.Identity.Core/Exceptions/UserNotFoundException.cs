namespace Confab.Modules.Identity.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class UserNotFoundException : ConfabNotFoundException
    {
        public UserNotFoundException(Guid id) 
            : base($"User with Id: {id} was not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}