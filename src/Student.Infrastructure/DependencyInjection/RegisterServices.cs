using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student.Domain.Entities;
using Student.Domain.Interfaces.Repositories;
using Student.Domain.Interfaces.Repositories.Common;
using Student.Domain.Interfaces.Services;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories;
using Student.Infrastructure.Persistence.Repositories.Common;
using Student.Infrastructure.Services;
using Student.Infrastructure.Storage;
using Azure.Storage.Blobs;

namespace Student.Infrastructure.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabaseContext(services, configuration);
        AddIdentity(services);
        AddRepositories(services);
        AddServices(services);
        AddAzureStorageService(services, configuration);

        return services;
    }
    
    
    
    private static void AddDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"), 
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                      .EnableSensitiveDataLogging()
                      .EnableDetailedErrors();
            });
    }
    private static void AddIdentity(IServiceCollection services)
    {
        services.AddIdentityCore<SchoolUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddTransient<ICourseRepository, CourseRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
    }
    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IJwtTokenService, JwtTokenService>();
        services.AddTransient<IAuthManager, AuthManager>();
    }

    private static void AddAzureStorageService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStorageService, AzureStorageService>();
        services.AddSingleton(_ => new BlobServiceClient(configuration["AzureStorageSettings:ConnectionString"]));
    }
}
