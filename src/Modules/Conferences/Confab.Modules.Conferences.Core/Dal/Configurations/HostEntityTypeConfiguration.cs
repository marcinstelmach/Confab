namespace Confab.Modules.Conferences.Core.Dal.Configurations
{
    using Confab.Modules.Conferences.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class HostEntityTypeConfiguration : IEntityTypeConfiguration<Host>
    {
        public void Configure(EntityTypeBuilder<Host> builder)
        {
            builder.ToTable("Hosts");
            builder.HasKey(x => x.Id);
        }
    }
}