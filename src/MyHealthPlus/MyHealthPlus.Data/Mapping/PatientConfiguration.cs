using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Mapping
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients")
                .HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired();

            builder.Property(x => x.LastName)
                .IsRequired();

            builder.HasOne(x => x.Appointment)
                   .WithMany();
        }
    }
}