namespace Confab.Modules.Tickets.Core.Dal.Configurations
{
    using Confab.Modules.Tickets.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketSaleConfiguration : IEntityTypeConfiguration<TicketSale>
    {
        public void Configure(EntityTypeBuilder<TicketSale> builder)
        {
            builder.ToTable("TicketSales");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Tickets)
                .WithOne(x => x.TicketSale)
                .HasForeignKey(x => x.TicketSaleId);
        }
    }
}