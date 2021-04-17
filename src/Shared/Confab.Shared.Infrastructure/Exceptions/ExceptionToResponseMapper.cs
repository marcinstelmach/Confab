namespace Confab.Shared.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Concurrent;
    using System.Net;
    using Confab.Shared.Abstractions.Exceptions;
    using Humanizer;

    internal class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        private static readonly ConcurrentDictionary<Type, string> Codes = new();

        public ExceptionResponse Map(Exception exception)
        {
            return exception switch
            {
                ConfabException => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(exception), exception.Message)), HttpStatusCode.BadRequest),
                ConfabNotFoundException => new ExceptionResponse(new ErrorsResponse(new Error(GetErrorCode(exception), exception.Message)), HttpStatusCode.NotFound),
                _ => new ExceptionResponse(new ErrorsResponse(new Error("InternalError", "Something went wrong")), HttpStatusCode.InternalServerError)
            };
        }

        private record Error(string Code, string Message);

        private record ErrorsResponse(params Error[] Errors);

        private static string GetErrorCode(Exception exception)
        {
            var type = exception.GetType();
            var code = type.Name.Underscore().Replace("_exception", string.Empty);
            return Codes.GetOrAdd(type, code);
        }
    }
}