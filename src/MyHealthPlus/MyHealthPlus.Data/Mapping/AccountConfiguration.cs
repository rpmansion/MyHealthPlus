using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Mapping
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account))
                .HasKey(x => x.Id);

            builder.HasIndex(x => x.UserName)
                .IsUnique();

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.UserName)
                .IsRequired();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.SecurityStamp)
                .IsRequired();
        }
    }
}