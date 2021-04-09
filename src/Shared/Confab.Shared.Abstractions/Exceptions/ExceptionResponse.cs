namespace Confab.Shared.Abstractions.Exceptions
{
    using System.Net;

    public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
}