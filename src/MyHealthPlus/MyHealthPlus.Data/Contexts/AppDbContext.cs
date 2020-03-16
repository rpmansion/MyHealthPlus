﻿using Microsoft.EntityFrameworkCore;
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

        public DbSet<AccountRole> AccountRoles { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(AccountConfiguration).Assembly);
        }
    }
}