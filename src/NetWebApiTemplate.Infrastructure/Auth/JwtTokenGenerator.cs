using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NetWebApiTemplate.Infrastructure.Auth
{
    public class JwtTokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GenerateAccessToken(List<Claim> claims)
        {
            return Task.Run(() =>
            {
                var jwtSetting = _configuration.GetSection("Jwt").Get<JwtSetting>() ?? throw AppExceptionFactory.JwtIsNotDefined;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: jwtSetting.Issuer,
                    audience: jwtSetting.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(jwtSetting.RefreshTokenExpiryDays),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            });
        }

        public Task<string> GenerateRefreshToken()
        {
            return Task.Run(() =>
            {
                var randomNumber = new byte[64];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            });
        }
    }
}
