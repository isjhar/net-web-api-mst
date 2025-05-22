using NewWebApiTemplate.Application.Interfaces;

namespace NewWebApiTemplate.Infrastructure.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string password)
        {
            return Task.Run(() =>
            {
                return BCrypt.Net.BCrypt.HashPassword(password); ;
            });
        }

        public Task<bool> VerifyPassowrdAsync(string password, string hashedPassword)
        {
            return Task.Run(() =>
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            });
        }
    }
}
