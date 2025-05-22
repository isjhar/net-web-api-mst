using Microsoft.EntityFrameworkCore;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Interfaces;
using NewWebApiTemplate.Application.Repositories;
using NewWebApiTemplate.Domain.Entities;
using NewWebApiTemplate.Persistence.Contexts;
using NewWebApiTemplate.Persistence.Mappers;

namespace NewWebApiTemplate.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(AppDbContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<User?> AuthenticateAsync(UserCredentialsDto credentials)
        {
            User? result = null;
            var userRow = await context.Users
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Username == credentials.Username);

            if (userRow != null)
            {
                if (await _passwordHasher.VerifyPassowrdAsync(credentials.Password, userRow.Password))
                {
                    var roles = userRow.Roles.Select(c => new Role
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Permissions = c.Permissions.Select(p => PermissionKeyMapper.ToDomain(p.Key)).ToList()
                    }).ToList();

                    result = new User
                    {
                        Id = userRow.Id,
                        Email = userRow.Email,
                        Name = userRow.Name,
                        Username = userRow.Username,
                        Roles = roles,
                    };
                }
            }

            return result;
        }

        public async Task<User?> FindById(Guid id)
        {
            User? result = null;
            var userRow = await context.Users
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (userRow != null)
            {
                var roles = userRow.Roles.Select(c => new Role
                {
                    Id = c.Id,
                    Name = c.Name,
                    Permissions = c.Permissions.Select(p => PermissionKeyMapper.ToDomain(p.Key)).ToList()
                }).ToList();

                result = new User
                {
                    Id = userRow.Id,
                    Email = userRow.Email,
                    Name = userRow.Name,
                    Username = userRow.Username,
                    Roles = roles,
                };
            }

            return result;
        }
    }
}
