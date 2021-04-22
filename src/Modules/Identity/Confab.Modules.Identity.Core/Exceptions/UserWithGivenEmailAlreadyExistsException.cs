namespace Confab.Modules.Identity.Core.Exceptions
{
    using Confab.Shared.Abstractions.Exceptions;

    public class UserWithGivenEmailAlreadyExistsException : ConfabException
    {
        public UserWithGivenEmailAlreadyExistsException(string email) 
            : base($"User with email: '{email}' already exists")
        {
            Email = email;
        }

        public string Email { get; }
    }
}