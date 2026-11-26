using Microsoft.Extensions.DependencyInjection;

namespace NEXORA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services;
    }
}