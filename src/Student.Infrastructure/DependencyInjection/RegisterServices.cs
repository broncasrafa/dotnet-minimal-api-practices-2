using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student.Infrastructure.Persistence.Context;

namespace Student.Infrastructure.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), 
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();
            });
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services;
    }
}
