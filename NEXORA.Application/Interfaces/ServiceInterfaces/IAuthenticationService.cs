namespace NEXORA.Application.Interfaces.ServiceInterfaces;

public interface IAuthenticationService
{
    
    public Task<string> Authenticate(string email, string password);
    
}