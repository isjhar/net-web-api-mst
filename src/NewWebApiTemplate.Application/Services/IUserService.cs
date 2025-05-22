using NewWebApiTemplate.Application.Dtos;

namespace NewWebApiTemplate.Application.Services
{
    public interface IUserService
    {
        Task<PairTokenDto> LoginAsync(UserCredentialsDto auth);
        Task<PairTokenDto> ReloginAsync(string refreshToken);
    }
}
