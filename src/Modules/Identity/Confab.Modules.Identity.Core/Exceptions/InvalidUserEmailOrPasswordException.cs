namespace Confab.Modules.Identity.Core.Exceptions
{
    using Confab.Shared.Abstractions.Exceptions;

    public class InvalidUserEmailOrPasswordException : ConfabException
    {
        public InvalidUserEmailOrPasswordException() 
            : base("Invalid user email or password")
        {
        }
    }
}