namespace Confab.Shared.Infrastructure.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    internal interface IExceptionCompositionRoot
    {
        ExceptionResponse Map(Exception exception);
    }
}