using System;
using System.Collections.Generic;
using Confab.Modules.Agendas.Domain.Submissions.Extensions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Submission : AggregateRoot
    {
        private List<Speaker> _speakers;

        public Submission(AggregateId id, ConferenceId conferenceId, string title, string description, int level, string status, IEnumerable<string> tags, IEnumerable<Speaker> speakers, int version = 0)
            : this(id, conferenceId)
        {
            Title = title;
            Description = description;
            Level = level;
            Status = status;
            Tags = tags;
            _speakers = new List<Speaker>(speakers);
            Version = version;
        }

        public Submission(AggregateId id, ConferenceId conferenceId)
        {
            Id = id;
            ConferenceId = conferenceId;
        }

        public ConferenceId ConferenceId { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int Level { get; private set; }

        public string Status { get; private set; }

        public IEnumerable<string> Tags { get; private set; }

        public IReadOnlyCollection<Speaker> Speakers => _speakers;

        public static Submission Create(AggregateId id, ConferenceId conferenceId, string title, string description, int level, IEnumerable<string> tags, IEnumerable<Speaker> speakers)
        {
            var submission = new Submission(id, conferenceId);

            return submission;
        }

        public void ChangeTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new EmptySubmissionTitleException(Id);
            }
            
            
        }
    }
}