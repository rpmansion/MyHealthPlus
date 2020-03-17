using Microsoft.EntityFrameworkCore;
using MyHealthPlus.Data.Mapping;
using MyHealthPlus.Data.Models;

namespace MyHealthPlus.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Account2Role> AccountRoles { get; set; }

        public DbSet<AccountProfile> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
        }
    }
}