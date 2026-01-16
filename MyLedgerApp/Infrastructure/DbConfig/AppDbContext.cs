using Microsoft.EntityFrameworkCore;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.DbConfig
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);
        }
    }
}
