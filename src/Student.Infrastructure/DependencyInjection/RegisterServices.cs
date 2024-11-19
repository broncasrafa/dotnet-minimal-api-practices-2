using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student.Infrastructure.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}
