namespace Confab.Modules.Tickets.Core.Dal.Configurations
{
    using Confab.Modules.Tickets.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ConferenceEntityTypeConfiguration : IEntityTypeConfiguration<Conference>
    {
        public void Configure(EntityTypeBuilder<Conference> builder)
        {
            builder.ToTable("Conferences");
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.TicketSales)
                .WithOne()
                .HasForeignKey(x => x.ConferenceId);
        }
    }
}