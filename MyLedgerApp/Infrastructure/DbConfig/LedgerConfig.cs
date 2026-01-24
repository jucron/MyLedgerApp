using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLedgerApp.Domain.Entities;
using MyLedgerApp.Domain.Entities.Users;

namespace MyLedgerApp.Infrastructure.DbConfig
{
    public class LedgerConfig : IEntityTypeConfiguration<Ledger>
    {
        public void Configure(EntityTypeBuilder<Ledger> builder)
        {
            builder.ToTable("Ledgers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.CurrentBalance)
                .IsRequired()
                .HasPrecision(18,4);

            builder.HasMany<Transaction>(x => x.Transactions)
                .WithOne(x => x.Ledger)
                .HasForeignKey(x => x.LedgerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Client>(x => x.Client)
                .WithMany(x => x.Ledgers)
                .HasForeignKey(x => x.ClientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Employee>
                (x => x.Employee)
                .WithMany(x => x.Ledgers)
                .HasForeignKey(x => x.EmployeeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.ClientId);
            builder.HasIndex(x => x.EmployeeId);

        }
    }
}
