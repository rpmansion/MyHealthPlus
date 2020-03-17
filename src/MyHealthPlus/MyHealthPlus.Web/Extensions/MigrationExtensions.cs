using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyHealthPlus.Data.Contexts;
using MyHealthPlus.Data.Enums;
using MyHealthPlus.Data.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace MyHealthPlus.Web.Extensions
{
    public static class MigrationExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateMigration(scope);
                CreateRoles(scope);
                CreateAccountsAsync(scope);
                CreateAppointments(scope);
            }

            return host;
        }

        private static void CreateMigration(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void CreateRoles(IServiceScope scope)
        {
            using (var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>())
            {
                var roles = new[]
                {
                    new {Role = "Patient", Description = "Standard user."},
                    new {Role = "Doctor", Description = ""},
                    new {Role = "Admin", Description = ""}
                };

                foreach (var role in roles)
                {
                    var r = roleManager.FindByNameAsync(role.Role).Result;

                    if (r == null)
                    {
                        var newRole = new Role
                        {
                            Name = role.Role,
                            Description = role.Description
                        };

                        var result = roleManager.CreateAsync(newRole).Result;

                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                    }
                }
            }
        }

        private static void CreateAccountsAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();
            var accounts = new[]
            {
                    new {UserName = "JohnDoe", Role = "Patient"},
                    new {UserName = "JaneDoe", Role = "Patient" },
                    new {UserName = "MarkStephenson", Role = "Doctor" },
                    new {UserName = "AdminTeam", Role = "Admin"}
                };

            foreach (var account in accounts)
            {
                var acct = userManager.FindByNameAsync(account.UserName).Result;

                if (acct == null)
                {
                    var newAcct = new Account
                    {
                        UserName = account.UserName
                    };

                    var result = userManager.CreateAsync(newAcct, "Pass123$").Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    //result = userManager.AddClaimsAsync(newAcct, new Claim[]{
                    //    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    //    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    //    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    //    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com")
                    //}).Result;

                    //if (!result.Succeeded)
                    //{
                    //    throw new Exception(result.Errors.First().Description);
                    //}

                    var addRoleResult = userManager.AddToRoleAsync(newAcct, account.Role).Result;

                    if (!addRoleResult.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Debug.WriteLine($"{account.UserName} created");
                }
            }
        }

        private static void CreateAppointments(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            using (var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>())
            {
                var accounts = userManager.GetUsersInRoleAsync("Patient").Result;

                if (accounts != null && accounts.Any())
                {
                    foreach (var account in accounts)
                    {
                        var exist = context.Appointments
                            .FirstOrDefaultAsync(x => x.Account == account
                                   && x.Date.Date == DateTime.Now.Date);

                        var hoursCounter = 8;
                        
                        if (exist == null)
                        {
                            var appointment = new Appointment
                            {
                                CheckupType = CheckupType.General,
                                Date = DateTime.Now,
                                Time = DateTime.Now.AddHours(hoursCounter++),
                                Account = account
                            };

                            context.Appointments.Add(appointment);
                            context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}