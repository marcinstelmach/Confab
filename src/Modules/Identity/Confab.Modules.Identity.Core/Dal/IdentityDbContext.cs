namespace Confab.Modules.Identity.Core.Dal
{
    using Confab.Modules.Identity.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class IdentityDbContext : DbContext, IUnitOfWork
    {
        private const string UsersSchemaName = "Identity";

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(UsersSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}