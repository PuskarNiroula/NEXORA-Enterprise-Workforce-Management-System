using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEXORA.Application.Interfaces.RepositoryInterfaces;
using NEXORA.Application.Interfaces.ServiceInterfaces;
using NEXORA.Infrastructure.Data;
using NEXORA.Infrastructure.Repository;
using NEXORA.Infrastructure.Security;

namespace NEXORA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtHelper>();
        services.AddScoped<IDepartmentRepository,DepartmentRepository>();

        return services;
    }
}