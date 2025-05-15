namespace NewWebApiTemplate.Application.Interfaces
{
    public interface IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string password);

        public Task<bool> VerifyPassowrdAsync(string password, string hashedPassword);
    }
}
