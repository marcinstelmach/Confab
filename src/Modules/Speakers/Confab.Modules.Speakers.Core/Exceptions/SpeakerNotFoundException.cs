namespace Confab.Modules.Speakers.Core.Exceptions
{
    using System;
    using Confab.Shared.Abstractions.Exceptions;

    public class SpeakerNotFoundException : ConfabNotFoundException
    {
        public SpeakerNotFoundException(Guid id)
            : base($"Speaker with Id: '{id}'  was not found")
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}