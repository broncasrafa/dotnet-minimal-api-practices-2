using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Student.Application.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
