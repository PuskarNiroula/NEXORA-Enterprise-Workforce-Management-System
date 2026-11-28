using NEXORA.Domain.Entities;

namespace NEXORA.Application.Interfaces.RepositoryInterfaces;

public interface IUserRepository
{
    public Task<User> GetUserByEmailAsync(string email);
    
}