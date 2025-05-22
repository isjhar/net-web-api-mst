using NewWebApiTemplate.Application.Dtos;

namespace NewWebApiTemplate.Application.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task StoreRefreshTokenAsync(StoreRefreshTokenDto data);
        Task<Guid?> FindUserIdByRefreshToken(string refreshToken);
        Task RevokeRefreshToken(string refreshToken);
    }
}