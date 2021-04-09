namespace Confab.Shared.Abstractions.Exceptions
{
    using System;

    public interface IExceptionToResponseMapper
    {
        ExceptionResponse Map(Exception exception);
    }
}