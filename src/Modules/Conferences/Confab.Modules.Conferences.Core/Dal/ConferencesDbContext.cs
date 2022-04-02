namespace Confab.Modules.Conferences.Core.Dal
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Confab.Modules.Conferences.Core.Entities;
    using Confab.Shared.Abstractions.Events;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Shared.Abstractions.Messaging;

    internal class ConferencesDbContext : DbContext, IUnitOfWork
    {
        private const string ConferencesSchemaName = "Conferences";

        private readonly IMessageBroker _messageBroker;

        public ConferencesDbContext(DbContextOptions<ConferencesDbContext> options, IMessageBroker messageBroker)
            : base(options)
        {
            _messageBroker = messageBroker;
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Host> Hosts { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var affectedRows = await base.SaveChangesAsync(cancellationToken);

            var events = this.ChangeTracker.Entries<EventEntity>()
                .Where(x => x.Entity.Events is not null && x.Entity.Events.Any())
                .SelectMany(x => x.Entity.Events)
                .Cast<IMessage>()
                .ToArray();
            
            await _messageBroker.PublishAsync(events);
            return affectedRows;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(ConferencesSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}