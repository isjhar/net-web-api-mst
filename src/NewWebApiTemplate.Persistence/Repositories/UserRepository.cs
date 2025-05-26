using Microsoft.EntityFrameworkCore;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
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

        public async Task<User> AuthenticateAsync(UserCredentialsDto credentials)
        {
            var userRow = await context.Users
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Username == credentials.Username) ?? throw AppExceptionFactory.EntityNotFound;

            if (!await _passwordHasher.VerifyPassowrdAsync(credentials.Password, userRow.Password))
            {
                throw AppExceptionFactory.EntityNotFound;
            }

            var roles = userRow.Roles.Select(c => new Role
            {
                Id = c.Id,
                Name = c.Name,
                Permissions = c.Permissions.Select(p => PermissionKeyMapper.ToDomain(p.Key)).ToList()
            }).ToList();

            return new User
            {
                Id = userRow.Id,
                Email = userRow.Email,
                Name = userRow.Name,
                Username = userRow.Username,
                Roles = roles,
            };
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            var userRow = await context.Users
                .Include(u => u.Roles)
                    .ThenInclude(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == id) ?? throw AppExceptionFactory.EntityNotFound;

            var roles = userRow.Roles.Select(c => new Role
            {
                Id = c.Id,
                Name = c.Name,
                Permissions = c.Permissions.Select(p => PermissionKeyMapper.ToDomain(p.Key)).ToList()
            }).ToList();

            return new User
            {
                Id = userRow.Id,
                Email = userRow.Email,
                Name = userRow.Name,
                Username = userRow.Username,
                Roles = roles,
            };
        }
    }
}
