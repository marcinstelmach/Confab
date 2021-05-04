namespace Confab.Modules.Tickets.Core.Entities
{
    using System;
    using Confab.Modules.Tickets.Core.Exceptions;

    public class Ticket
    {
        public Ticket(Guid conferenceId, decimal? price, TicketSale ticketSale, DateTime createdAt)
        {
            Id = Guid.NewGuid();
            ConferenceId = conferenceId;
            TicketSale = ticketSale;
            Price = price;
            Code = Guid.NewGuid().ToString("N");
            CreatedAt = createdAt;
        }

        private Ticket()
        {
        }

        public Guid Id { get; set; }

        public TicketSale TicketSale { get; set; }

        public Guid ConferenceId { get; set; }

        public Conference Conference { get; set; }

        public decimal? Price { get; set; }

        public string Code { get; set; }

        public Guid? UserId { get; private set; }

        public DateTime? PurchasedAt { get; private set; }

        public DateTime? UsedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public void Purchase(Guid userId, DateTime purchasedAt, decimal? price)
        {
            if (UserId.HasValue)
            {
                throw new TicketAlreadyPurchasedException(ConferenceId, UserId.Value);
            }

            UserId = userId;
            PurchasedAt = purchasedAt;
            Price = price;
        }
    }
}