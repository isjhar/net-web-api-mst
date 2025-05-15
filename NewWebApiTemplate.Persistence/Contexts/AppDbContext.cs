using Microsoft.EntityFrameworkCore;
using NewWebApiTemplate.Application.Interfaces;
using NewWebApiTemplate.Persistence.Entities;

namespace NewWebApiTemplate.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        private readonly IPasswordHasher _passwordHasher;

        public AppDbContext(DbContextOptions<AppDbContext> options, IPasswordHasher passwordHasher)
        : base(options)
        {
            _passwordHasher = passwordHasher;
        }
    }
}
