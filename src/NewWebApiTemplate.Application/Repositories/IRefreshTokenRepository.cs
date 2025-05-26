using NewWebApiTemplate.Application.Dtos;

namespace NewWebApiTemplate.Application.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task StoreRefreshTokenAsync(StoreRefreshTokenDto data);
        Task<Guid> FindUserIdByRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}