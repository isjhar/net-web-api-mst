using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Domain.Entities;

namespace NewWebApiTemplate.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AuthenticateAsync(UserCredentialsDto credentials);
        Task<User?> FindById(Guid id);
    }
}
