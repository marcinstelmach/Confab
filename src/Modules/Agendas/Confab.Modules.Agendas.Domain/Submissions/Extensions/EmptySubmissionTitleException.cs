using System;
using Confab.Shared.Abstractions.Exceptions;

namespace Confab.Modules.Agendas.Domain.Submissions.Extensions
{
    public class EmptySubmissionTitleException : ConfabException
    {
        public Guid SubmissionId { get; }
        
        public EmptySubmissionTitleException(Guid submissionId)
            : base($"Submission with {submissionId} was null")
        {
            SubmissionId = submissionId;
        }
    }
}