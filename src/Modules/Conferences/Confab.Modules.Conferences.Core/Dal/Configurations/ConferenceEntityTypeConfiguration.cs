namespace Confab.Modules.Conferences.Core.Dal.Configurations
{
    using System;
    using Confab.Modules.Conferences.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ConferenceEntityTypeConfiguration : IEntityTypeConfiguration<Conference>
    {
        public void Configure(EntityTypeBuilder<Conference> builder)
        {
            builder.ToTable("Conferences");
            builder.HasKey(x => x.Id);

            builder.Property<Guid>("HostId");

            builder.HasOne(x => x.Host)
                .WithMany(x => x.Conferences)
                .HasForeignKey("HostId");

            builder.Ignore(x => x.Events);
        }
    }
}