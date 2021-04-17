namespace Confab.Shared.Abstractions.Exceptions
{
    using System;

    public abstract class ConfabNotFoundException : Exception
    {
        protected ConfabNotFoundException(string message)
            : base(message)
        {
        }
    }
}