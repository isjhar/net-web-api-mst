using System.Security.Claims;

namespace NewWebApiTemplate.Application.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateAccessToken(List<Claim> claims);
        Task<string> GenerateRefreshToken();
    }
}
