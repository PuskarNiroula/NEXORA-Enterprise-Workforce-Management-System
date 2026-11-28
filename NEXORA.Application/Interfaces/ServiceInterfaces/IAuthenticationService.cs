namespace NEXORA.Application.Interfaces;

public interface IAuthenticationService
{
    
    public Task<string> Authenticate(string email, string password);
    
}