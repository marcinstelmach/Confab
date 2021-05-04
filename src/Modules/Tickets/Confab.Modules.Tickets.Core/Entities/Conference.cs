namespace Confab.Modules.Tickets.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class Conference
    {
        public Conference(Guid id, string name, int? participantsLimit, DateTimeOffset @from, DateTimeOffset to)
        {
            Id = id;
            Name = name;
            ParticipantsLimit = participantsLimit;
            From = @from;
            To = to;
        }

        private Conference()
        {
        }

        public Guid Id { get; }

        public string Name { get; set; }

        public int? ParticipantsLimit { get; set; }

        public DateTimeOffset From { get; set; }

        public DateTimeOffset To { get; set; }

        public ICollection<TicketSale> TicketSales { get; set; }
    }
}