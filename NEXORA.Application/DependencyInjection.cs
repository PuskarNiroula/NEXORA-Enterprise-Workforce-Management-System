using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NEXORA.Application.Interfaces.ServiceInterfaces;
using NEXORA.Application.Services;
using NEXORA.Domain.Entities;

namespace NEXORA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService,AuthenticationService>();
        services.AddScoped<IDepartmentService,DepartmentService>();

        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }
}