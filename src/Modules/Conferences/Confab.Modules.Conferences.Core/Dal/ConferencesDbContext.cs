using Confab.Shared.Abstractions.Modules;

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
        private readonly IModuleClient _moduleClient;

        public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options, IEventDispatcher eventDispatcher, IModuleClient moduleClient)
            : base(options)
        {
            _eventDispatcher = eventDispatcher;
            _moduleClient = moduleClient;
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Host> Hosts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var affectedRows = await base.SaveChangesAsync(cancellationToken);
            await _moduleClient.PublishAsync(this);
            return affectedRows;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ConferencesSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}