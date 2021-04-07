namespace Confab.Modules.Conferences.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class CannotDeleteConferenceException : ConfabException
    {
        public CannotDeleteConferenceException(Guid id)
            : base($"Cannot delete conference with Id: '{id}'.")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}