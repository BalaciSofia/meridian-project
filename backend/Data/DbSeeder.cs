using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    // This class fills the database with some starting data.
    // It runs every time the app starts (see Program.cs), but it only
    // adds data if the tables are empty, so it is safe to run more than once.
    public static class DbSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            await SeedRolesAsync(context);
            await SeedDepartmentsAsync(context);
            await SeedDocumentTypesAsync(context);
            await SeedAccountsAsync(context);
            await SeedPostsAsync(context);
        }

        private static async Task SeedRolesAsync(AppDbContext context)
        {
            if (await context.Roles.AnyAsync())
            {
                return;
            }

            context.Roles.AddRange(
                new Role { RoleName = "Employee" },
                new Role { RoleName = "HR" },
                new Role { RoleName = "Admin" }
            );

            await context.SaveChangesAsync();
        }

        private static async Task SeedDepartmentsAsync(AppDbContext context)
        {
            if (await context.Departments.AnyAsync())
            {
                return;
            }

            context.Departments.AddRange(
                new Department { DepartmentName = "Engineering", SlackLink = "https://meridian.slack.com/channels/engineering" },
                new Department { DepartmentName = "Sales", SlackLink = "https://meridian.slack.com/channels/sales" },
                new Department { DepartmentName = "Marketing", SlackLink = "https://meridian.slack.com/channels/marketing" },
                new Department { DepartmentName = "HR", SlackLink = "https://meridian.slack.com/channels/hr" },
                new Department { DepartmentName = "Finance", SlackLink = "https://meridian.slack.com/channels/finance" }
            );

            await context.SaveChangesAsync();
        }

        private static async Task SeedDocumentTypesAsync(AppDbContext context)
        {
            if (await context.DocumentTypes.AnyAsync())
            {
                return;
            }

            context.DocumentTypes.AddRange(
                new DocumentType
                {
                    Name = "ID Card Copy",
                    Description = "A copy of your government-issued ID.",
                    RequiredForAll = true
                },
                new DocumentType
                {
                    Name = "Signed Contract",
                    Description = "Your signed employment contract.",
                    RequiredForAll = true
                },
                new DocumentType
                {
                    Name = "Bank Details Form",
                    Description = "Form with your bank account details, used for payroll.",
                    RequiredForAll = true
                }
            );

            await context.SaveChangesAsync();
        }

        private static async Task SeedAccountsAsync(AppDbContext context)
        {
            if (await context.Accounts.AnyAsync())
            {
                return;
            }

            var hrRole = await context.Roles.FirstAsync(r => r.RoleName == "HR");
            var employeeRole = await context.Roles.FirstAsync(r => r.RoleName == "Employee");
            var hrDepartment = await context.Departments.FirstAsync(d => d.DepartmentName == "HR");
            var engineeringDepartment = await context.Departments.FirstAsync(d => d.DepartmentName == "Engineering");

            var passwordHasher = new PasswordHasher<Account>();

            // HR account. Use this one to log in and see the HR-only pages
            // (New Employee, Employees, Document Types).
            var hrAccount = new Account
            {
                Email = "hr@meridian.com",
                FirstName = "Hannah",
                LastName = "Reyes",
                RoleId = hrRole.Id,
                DepartmentId = hrDepartment.Id,
                MustChangePassword = false
            };
            hrAccount.PasswordHash = passwordHasher.HashPassword(hrAccount, "Meridian123!");

            // A regular new-employee account. Use this one to see what a brand
            // new hire sees on their first login (forced password change,
            // required documents, etc).
            var newHireAccount = new Account
            {
                Email = "newhire@meridian.com",
                FirstName = "Alex",
                LastName = "Popescu",
                RoleId = employeeRole.Id,
                DepartmentId = engineeringDepartment.Id,
                MustChangePassword = true
            };
            newHireAccount.PasswordHash = passwordHasher.HashPassword(newHireAccount, "Welcome123!");

            context.Accounts.AddRange(hrAccount, newHireAccount);
            await context.SaveChangesAsync();

            // Give the new hire the same required documents that a real
            // sign-up through the app would get, so the Profile page is not empty.
            var documentTypes = await context.DocumentTypes
                .Where(dt => dt.RequiredForAll || dt.RequiredForDepartmentId == newHireAccount.DepartmentId)
                .ToListAsync();

            foreach (var documentType in documentTypes)
            {
                context.Documents.Add(new Document
                {
                    AccountId = newHireAccount.Id,
                    DocumentTypeId = documentType.Id
                });
            }

            await context.SaveChangesAsync();
        }

        private static async Task SeedPostsAsync(AppDbContext context)
        {
            if (await context.Posts.AnyAsync())
            {
                return;
            }

            var hrAccount = await context.Accounts.FirstAsync(a => a.Email == "hr@meridian.com");

            context.Posts.Add(new Post
            {
                Title = "Welcome to Meridian!",
                Content = "We're happy to have you here. Check your Profile page for the documents you need to submit, and say hi in the calendar to see who is in the office this week.",
                CreatedByAccountId = hrAccount.Id
            });

            await context.SaveChangesAsync();
        }
    }
}
