namespace Confab.Modules.Speakers.Core.Dal
{
    using Confab.Modules.Speakers.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class SpeakersEntityTypeConfiguration : IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder.ToTable("Speakers");
            builder.HasKey(x => x.Id);
        }
    }
}