using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Mapping
{
    public class Account2RoleConfiguration : IEntityTypeConfiguration<Account2Role>
    {
        public void Configure(EntityTypeBuilder<Account2Role> builder)
        {
            builder.ToTable(nameof(Account2Role))
                .HasKey(x => x.Id);
        }
    }
}