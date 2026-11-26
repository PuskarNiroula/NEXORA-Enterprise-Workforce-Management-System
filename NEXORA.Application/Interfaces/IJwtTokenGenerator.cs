namespace NEXORA.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email, string fullName);
}