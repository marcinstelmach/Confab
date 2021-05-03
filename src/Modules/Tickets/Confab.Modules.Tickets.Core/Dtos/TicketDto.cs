namespace Confab.Modules.Tickets.Core.Dtos
{
    using System;

    public record TicketDto(string Code, decimal? Price, DateTime PurchasedAt, ConferenceDto Conference);
}