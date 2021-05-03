namespace Confab.Modules.Tickets.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class Conference
    {
        public Conference(Guid id, string name, int? participantsLimit, DateTime @from, DateTime to)
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

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public ICollection<TicketSale> TicketSales { get; set; }
    }
}