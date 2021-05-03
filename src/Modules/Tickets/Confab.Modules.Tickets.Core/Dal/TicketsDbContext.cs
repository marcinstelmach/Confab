namespace Confab.Modules.Tickets.Core.Dal
{
    using Confab.Modules.Tickets.Core.Entities;
    using Confab.Shared.Abstractions.Repositories;
    using Microsoft.EntityFrameworkCore;

    public class TicketsDbContext : DbContext, IUnitOfWork
    {
        public TicketsDbContext(DbContextOptions<TicketsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketSale> TicketSales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Tickets");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}