using Microsoft.EntityFrameworkCore;
using NEXORA.Application.Exception;
using NEXORA.Application.Interfaces;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Domain.Entities;
using NEXORA.Infrastructure.Data;

namespace NEXORA.Infrastructure.Repository;

public class UserRepository(ApplicationDbContext context):IUserRepository
{
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var user = await context.Users.Where(u => u.Email == email)
            .Select(u=>new User
            {
                Id = u.Id,
                Email = u.Email,
                FullName = u.FullName,
                PasswordHash = u.PasswordHash,
                
            })
            .FirstAsync();
        
        return user ?? throw new UserNotFoundException();
    }
}