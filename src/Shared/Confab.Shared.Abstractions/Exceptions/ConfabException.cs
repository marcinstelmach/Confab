namespace Confab.Shared.Abstractions.Exceptions
{
    using System;

    public abstract class ConfabException : Exception
    {
        protected ConfabException(string message)
            : base(message)
        {
        }
    }
}