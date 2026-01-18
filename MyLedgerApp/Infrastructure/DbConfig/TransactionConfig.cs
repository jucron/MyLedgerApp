using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.DbConfig
{
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.Ledger)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.LedgerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.LedgerId);

        }
    }
}
