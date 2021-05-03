namespace Confab.Modules.Tickets.Core.Dal.Configurations
{
    using Confab.Modules.Tickets.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class TicketEntityTypeConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x => x.UserId).IsConcurrencyToken();
        }
    }
}