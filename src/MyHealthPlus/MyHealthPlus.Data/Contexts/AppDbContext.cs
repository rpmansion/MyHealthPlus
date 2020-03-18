using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyHealthPlus.Data.Mapping;
using MyHealthPlus.Data.Models;
using System.Threading.Tasks;

namespace MyHealthPlus.Data.Contexts
{
    public class AppDbContext : DbContext, IPersistedGrantDbContext
    {
        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public AppDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountClaim> AccountClaims { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Account2Role> AccountRoles { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }

        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
            builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
        }
    }
}