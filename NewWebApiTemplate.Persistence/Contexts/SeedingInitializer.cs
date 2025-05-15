using Microsoft.EntityFrameworkCore;
using NewWebApiTemplate.Application.Interfaces;
using NewWebApiTemplate.Persistence.Entities;

namespace NewWebApiTemplate.Persistence.Contexts
{
    public static class SeedingInitializer
    {
        public static async Task Initialize(AppDbContext context, IPasswordHasher passwordHasher)
        {
            if (await context.Users.AnyAsync())
            {
                return;
            }
            var permissions = new List<Permission>
            {
                new() {
                    Key = "create.user",
                    Description = "Create user"
                },
                new() {
                    Key = "edit.user",
                    Description = "Edit user"
                },
                new() {
                    Key = "view.user",
                    Description = "View user"
                },
                new() {
                    Key = "delete.user",
                    Description = "Delete user"
                },
                new() {
                    Key = "create.role",
                    Description = "Create role"
                },
                new() {
                    Key = "edit.role",
                    Description = "Edit role"
                },
                new() {
                    Key = "view.role",
                    Description = "View role"
                },
                new() {
                    Key = "delete.role",
                    Description = "Delete role"
                },
            };

            var hashedPassword = await passwordHasher.HashPasswordAsync("1234");

            var user = new User
            {
                Username = "sysadmin",
                Email = "sysadmin@email.com",
                Name = "Sys Admin",
                Password = hashedPassword,
                Roles =
                [
                    new() {
                        Name = "Sys Admin",
                        Permissions = permissions,
                    }
                ]
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
