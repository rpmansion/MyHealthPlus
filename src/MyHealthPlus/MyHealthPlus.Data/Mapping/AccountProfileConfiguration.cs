using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Mapping
{
    public class AccountProfileConfiguration : IEntityTypeConfiguration<AccountProfile>
    {
        public void Configure(EntityTypeBuilder<AccountProfile> builder)
        {
            builder.ToTable(nameof(AccountProfile))
                .HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();
        }
    }
}