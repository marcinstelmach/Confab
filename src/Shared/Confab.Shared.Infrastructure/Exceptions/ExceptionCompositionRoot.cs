namespace Confab.Shared.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Confab.Shared.Abstractions.Exceptions;

    public class ExceptionCompositionRoot : IExceptionCompositionRoot
    {
        private readonly ICollection<IExceptionToResponseMapper> _mappers;

        public ExceptionCompositionRoot(IEnumerable<IExceptionToResponseMapper> mappers)
        {
            _mappers = mappers.ToList();
        }

        public ExceptionResponse Map(Exception exception)
        {
            var nonDefaultMappers = _mappers.Where(x => x is not ExceptionToResponseMapper);
            var exceptionResult = nonDefaultMappers
                .Select(x => x.Map(exception))
                .SingleOrDefault(x => x is not null);

            if (exceptionResult is not null)
            {
                return exceptionResult;
            }

            var defaultMapper = _mappers.SingleOrDefault(x => x is ExceptionToResponseMapper);
            return defaultMapper?.Map(exception);
        }
    }
}