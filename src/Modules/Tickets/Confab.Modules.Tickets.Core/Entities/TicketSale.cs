namespace Confab.Modules.Tickets.Core.Entities
{
    using System;
    using System.Collections.Generic;

    public class TicketSale
    {
        public TicketSale(Guid conferenceId, string name, decimal? price, int? amount, DateTime @from, DateTime to)
        {
            Id = Guid.NewGuid();
            ConferenceId = conferenceId;
            Name = name;
            Price = price;
            Amount = amount;
            From = @from;
            To = to;
        }

        public Guid Id { get; set; }

        public Guid ConferenceId { get; set; }

        public string Name { get; set; }

        public decimal? Price { get; set; }

        public int? Amount { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}