namespace NEXORA.Application.Interfaces.ServiceInterfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string email, string fullName);
}