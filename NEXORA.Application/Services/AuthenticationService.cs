using Microsoft.AspNetCore.Identity;
using NEXORA.Application.Interfaces;
using NEXORA.Domain.Entities;

namespace NEXORA.Application.Services;

public class AuthenticationService(
    IUserRepository repository,
    IPasswordHasher<User> passwordHasher,
    IJwtTokenGenerator jwtTokenGenerator
    ):IAuthenticationService
{
    public async Task<string> Authenticate(string email, string password)
    {
        try
        {
            var user = await repository.GetUserByEmailAsync(email);

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash!,
                password
            );

            if (result != PasswordVerificationResult.Failed)
            {
                return jwtTokenGenerator.GenerateToken(
                    user.Id,
                    user.Email,
                    user.FullName
                );

            }
        }
        catch (System.Exception e)
        {
            throw new System.Exception(e.Message);

        }
        throw new System.Exception("Invalid Email or Password");

    }
}