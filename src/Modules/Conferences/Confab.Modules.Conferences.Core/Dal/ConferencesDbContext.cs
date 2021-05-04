namespace Confab.Modules.Conferences.Core.Dal
{
    using System.Threading;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions.Events;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    internal class ConferencesDbContext : DbContext, IUnitOfWork
    {
        private const string ConferencesSchemaName = "Conferences";

        private readonly IEventDispatcher _eventDispatcher;

        public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options, IEventDispatcher eventDispatcher)
            : base(options)
        {
            _eventDispatcher = eventDispatcher;
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Host> Hosts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var affectedRows = await base.SaveChangesAsync(cancellationToken);
            await _eventDispatcher.DispatchAsync(this);
            return affectedRows;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ConferencesSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}