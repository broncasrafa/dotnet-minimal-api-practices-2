﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Student.Domain.Interfaces.Repositories;
using Student.Domain.Interfaces.Repositories.Common;
using Student.Infrastructure.Persistence.Context;
using Student.Infrastructure.Persistence.Repositories;
using Student.Infrastructure.Persistence.Repositories.Common;

namespace Student.Infrastructure.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabaseContext(services, configuration);
        AddRepositories(services);


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
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddTransient<ICourseRepository, CourseRepository>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IEnrollmentRepository, EnrollmentRepository>();
    }
}
