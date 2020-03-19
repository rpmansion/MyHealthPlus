using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Mapping
{
    public class AccountClaimConfiguration : IEntityTypeConfiguration<AccountClaim>
    {
        public void Configure(EntityTypeBuilder<AccountClaim> builder)
        {
            builder.ToTable(nameof(AccountClaim))
                .HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Value)
                .IsRequired();
        }
    }
}