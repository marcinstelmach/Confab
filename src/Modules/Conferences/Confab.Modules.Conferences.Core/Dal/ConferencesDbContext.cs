namespace Confab.Modules.Conferences.Core.Dal
{
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class ConferencesDbContext : DbContext, IUnitOfWork
    {
        private const string ConferencesSchemaName = "Conferences";

        public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Host> Hosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ConferencesSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}