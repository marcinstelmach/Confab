namespace Confab.Modules.Speakers.Core.Dal
{
    using Confab.Modules.Speakers.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class SpeakersDbContext : DbContext, IUnitOfWork
    {
        private const string SpeakersSchemaName = "Speakers";

        public SpeakersDbContext(DbContextOptions<SpeakersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Speaker> Speakers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema(SpeakersSchemaName);
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}