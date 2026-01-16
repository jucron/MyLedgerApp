using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.DbConfig
{
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasMany(c => c.Ledgers)
                .WithOne(l => l.Client)
                .HasForeignKey(l => l.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
