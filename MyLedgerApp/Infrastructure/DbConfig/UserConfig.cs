using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyLedgerApp.Domain.Entities;

namespace MyLedgerApp.Infrastructure.DbConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder
                .HasDiscriminator<string>("UserType")
                .HasValue<Client>("Client")
                .HasValue<Employee>("Employee");

            builder.OwnsOne(u => u.Credential, c =>
            {
                c.Property(p => p.Username).IsRequired();
            });

        }
    }
}
