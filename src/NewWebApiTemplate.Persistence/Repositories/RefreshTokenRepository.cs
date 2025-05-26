using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetWebApiTemplate.Infrastructure.Auth;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Repositories;
using NewWebApiTemplate.Persistence.Contexts;

namespace NewWebApiTemplate.Persistence.Repositories
{
    public class RefreshTokenRepository : BaseRepository, IRefreshTokenRepository
    {
        private readonly IConfiguration _configuration;
        public RefreshTokenRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        public async Task<Guid?> FindUserIdByRefreshToken(string refreshToken)
        {
            Guid? result = null;
            var row = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (row != null)
            {
                result = row.UserId;
            }
            return result;
        }

        public async Task RevokeRefreshToken(string refreshToken)
        {
            var row = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == refreshToken);
            if (row != null)
            {
                context.RefreshTokens.Remove(row);
                await context.SaveChangesAsync();
            }
        }

        public async Task StoreRefreshTokenAsync(StoreRefreshTokenDto data)
        {
            var jwtSetting = _configuration.GetSection("Jwt").Get<JwtSetting>() ?? throw AppExceptionFactory.JwtIsNotDefined;
            var expiresAt = DateTime.UtcNow.AddDays(jwtSetting.RefreshTokenExpiryDays);
            var row = await context.RefreshTokens.FirstOrDefaultAsync(d => d.UserId == data.UserId);
            if (row != null)
            {
                row.Token = data.Token;
                row.ExpiresAt = expiresAt;
            }
            else
            {
                await context.RefreshTokens.AddAsync(new Entities.RefreshToken
                {
                    Token = data.Token,
                    UserId = data.UserId,
                    ExpiresAt = DateTime.UtcNow.AddDays(jwtSetting.RefreshTokenExpiryDays),
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
