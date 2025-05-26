using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Interfaces;
using NewWebApiTemplate.Application.Repositories;
using NewWebApiTemplate.Domain.Entities;
using System.Security.Claims;

namespace NewWebApiTemplate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<PairTokenDto> LoginAsync(UserCredentialsDto auth)
        {
            var authenticatedUser = await _userRepository.AuthenticateAsync(auth) ?? throw AppExceptionFactory.UsernameOrPasswordIncorrect;

            return await GeneratePairToken(authenticatedUser);
        }

        public async Task<PairTokenDto> ReloginAsync(string refreshToken)
        {
            var userId = await _refreshTokenRepository.FindUserIdByRefreshToken(refreshToken) ?? throw AppExceptionFactory.InvalidParams;

            var authenticatedUser = await _userRepository.FindByIdAsync(userId) ?? throw AppExceptionFactory.InvalidParams;

            return await GeneratePairToken(authenticatedUser);
        }

        public async Task<UserInfoDto> FindUserInfoByIdAsync(Guid id)
        {
            var user = await _userRepository.FindByIdAsync(id) ?? throw AppExceptionFactory.InvalidParams;

            return new UserInfoDto
            {
                Id = user.Id,
                Name = user.Name,
            };
        }

        private async Task<PairTokenDto> GeneratePairToken(User user)
        {
            var accessToken = await _tokenGenerator.GenerateAccessToken(CreatePayload(user));
            var refreshToken = await _tokenGenerator.GenerateRefreshToken();

            await _refreshTokenRepository.StoreRefreshTokenAsync(new StoreRefreshTokenDto
            {
                Token = refreshToken,
                UserId = user.Id,
            });

            return new PairTokenDto { AccessToken = accessToken, RefreshToken = refreshToken };
        }

        private List<Claim> CreatePayload(User user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
            };

            foreach (var role in user.Roles)
            {
                result.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            foreach (var permission in user.Roles.SelectMany(r => r.Permissions).Distinct())
            {
                result.Add(new Claim("Permission", permission.ToString()));
            }

            return result;
        }
    }
}
