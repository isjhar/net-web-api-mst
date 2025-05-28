using Microsoft.AspNetCore.Identity;
using NewWebApiTemplate.Application.Constants;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Repositories;
using NewWebApiTemplate.Persistence.Contexts;
using NewWebApiTemplate.Persistence.Entities;

namespace NewWebApiTemplate.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Domain.Entities.User> AuthenticateAsync(UserCredentialsDto credentials)
        {
            var userRow = await _userManager.FindByNameAsync(credentials.Username) ?? throw AppExceptionFactory.EntityNotFound;

            var isValid = await _userManager.CheckPasswordAsync(userRow, credentials.Password);

            if (!isValid)
            {
                throw AppExceptionFactory.EntityNotFound;
            }

            return await CreateUserAsync(userRow);
        }

        public async Task<Domain.Entities.User> FindByIdAsync(Guid id)
        {
            var userRow = await _userManager.FindByIdAsync(id.ToString()) ?? throw AppExceptionFactory.EntityNotFound;

            return await CreateUserAsync(userRow);
        }

        private async Task<Domain.Entities.User> CreateUserAsync(ApplicationUser userRow)
        {
            var roleNameRows = await _userManager.GetRolesAsync(userRow) ?? throw AppExceptionFactory.EntityNotFound;

            var roles = new List<Domain.Entities.Role>();
            foreach (var roleNameRow in roleNameRows)
            {
                var roleRow = await _roleManager.FindByNameAsync(roleNameRow);
                if (roleRow != null)
                {
                    var permissions = new List<string>();

                    var claimRows = await _roleManager.GetClaimsAsync(roleRow);

                    var claimPermissions = claimRows.Where(claimRow => claimRow.Type == AppConstant.Permission).Select(claimRow => claimRow.Value).ToList();
                    permissions.AddRange(claimPermissions);

                    var role = new Domain.Entities.Role
                    {
                        Id = roleRow.Id,
                        Name = roleRow.Name ?? string.Empty,
                        Permissions = permissions,
                    };

                    roles.Add(role);
                }
            }

            return new Domain.Entities.User
            {
                Id = userRow.Id,
                Email = userRow.Email ?? string.Empty,
                Name = userRow.Name,
                Username = userRow.UserName ?? string.Empty,
                Roles = roles,
            };
        }
    }
}
