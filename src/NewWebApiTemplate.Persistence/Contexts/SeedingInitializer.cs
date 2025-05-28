using Microsoft.AspNetCore.Identity;
using NewWebApiTemplate.Application.Constants;
using NewWebApiTemplate.Domain.Enums;
using NewWebApiTemplate.Persistence.Entities;
using System.Security.Claims;

namespace NewWebApiTemplate.Persistence.Contexts
{
    public static class SeedingInitializer
    {
        public static async Task Initialize(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var sysAdminRole = "Sys Admin";
            var sysAdminEmail = "sysadmin@email.com";

            var role = await roleManager.FindByNameAsync(sysAdminRole);
            if (role == null)
            {
                role = new ApplicationRole { Name = sysAdminRole };
                await roleManager.CreateAsync(role);
            }

            var claims = new[]
            {
                new Claim(AppConstant.Permission, PermissionKey.CreateUser),
                new Claim(AppConstant.Permission, PermissionKey.EditUser),
                new Claim(AppConstant.Permission, PermissionKey.DeleteUser),
                new Claim(AppConstant.Permission, PermissionKey.ViewUser),
                new Claim(AppConstant.Permission, PermissionKey.CreateRole),
                new Claim(AppConstant.Permission, PermissionKey.EditRole),
                new Claim(AppConstant.Permission, PermissionKey.DeleteRole),
                new Claim(AppConstant.Permission, PermissionKey.ViewRole),
            };

            foreach (var claim in claims)
            {
                var roleClaims = await roleManager.GetClaimsAsync(role);
                if (!roleClaims.Any(rc => rc.Type == claim.Type && rc.Value == claim.Value))
                {
                    await roleManager.AddClaimAsync(role, claim);
                }
            }

            var user = await userManager.FindByEmailAsync(sysAdminEmail);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "sysadmin",
                    Email = "sysadmin@email.com",
                    Name = "Sys Admin",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                var createUserResult = await userManager.CreateAsync(user, "SysAdmin123!");

                if (createUserResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(role?.Name))
                    {
                        await userManager.AddToRoleAsync(user, role.Name);
                    }
                }
            }
        }
    }
}
