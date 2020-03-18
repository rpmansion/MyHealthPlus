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
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;

namespace MyHealthPlus.Web.Extensions
{
    public static class MigrationExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateMigration(scope);
                CreateRolesAsync(scope);
                CreateAccountsAsync(scope);
                CreateAppointmentsAsync(scope);
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

        private static void CreateRolesAsync(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            var roles = new[]
            {
                new {Role = "Patient", Description = "Hospital patient"},
                new {Role = "Doctor", Description = "Hospital doctor"},
                new {Role = "Staff", Description = "Hospital staff (e.g. administrative)"}
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

        private static void CreateAccountsAsync(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();
            var accounts = new[]
            {
                new
                {
                    UserName = "johndoe@email.com",
                    Role = "Patient",
                    FirstName = "John",
                    LastName = "Doe",
                    Contact = "504-621-8927"
                },
                new
                {
                    UserName = "janedoe@email.com", 
                    Role = "Patient",
                    FirstName = "Jane",
                    LastName = "Doe",
                    Contact = "504-621-8637"
                },
                new
                {
                    UserName = "markstephenson@email.com",
                    Role = "Doctor",
                    FirstName = "Mark",
                    LastName = "Stephenson",
                    Contact = "504-323-8103"
                },
                new
                {
                    UserName = "merissaperin@email.com", 
                    Role = "Staff",
                    FirstName = "Merissa",
                    LastName = "Perin",
                    Contact = "324-313-4675"
                }
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

                    result = userManager.AddClaimsAsync(newAcct, new Claim[]{
                        new Claim(JwtClaimTypes.Name, $"{account.FirstName} {account.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, account.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, account.LastName),
                        new Claim(JwtClaimTypes.Email, account.UserName),
                        new Claim(JwtClaimTypes.Role, account.Role),
                        new Claim(JwtClaimTypes.PhoneNumber, account.Contact) 
                    }).Result;

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    var addRoleResult = userManager.AddToRoleAsync(newAcct, account.Role).Result;

                    if (!addRoleResult.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    Debug.WriteLine($"{account.UserName} created");
                }
            }
        }

        private static void CreateAppointmentsAsync(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Account>>();

            var accounts = userManager.GetUsersInRoleAsync("Patient").Result;

            if (accounts != null && accounts.Any())
            {
                foreach (var account in accounts)
                {
                    var exist = context.Appointments
                        .FirstOrDefaultAsync(x => x.Account == account
                               && x.Date.Date == DateTime.UtcNow.Date).Result;

                    var hoursCounter = 8;

                    if (exist == null)
                    {
                        var appointment = new Appointment
                        {
                            CheckupType = CheckupType.General,
                            Date = DateTime.UtcNow,
                            Time = DateTime.UtcNow.AddHours(hoursCounter),
                            Status = AppointmentStatus.Pending,
                            Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                            Account = account
                        };

                        context.Appointments.Add(appointment);
                        context.SaveChangesAsync();
                    }

                    hoursCounter++;
                }
            }
        }
    }
}