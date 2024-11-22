using Microsoft.Extensions.DependencyInjection;
using Student.Application.Mappers;
using Student.Application.Services.Implementations;
using Student.Application.Services.Interfaces;
using Student.Application.Validators.Course;
using FluentValidation;

namespace Student.Application.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        AddServices(services);
        AddAutomapper(services);
        AddFluentValidation(services);
        return services;
    }


    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IEnrollmentService, EnrollmentService>();
        services.AddScoped<IAccountService, AccountService>();
    }
    private static void AddAutomapper(IServiceCollection services) 
    {
        services.AddAutoMapper(typeof(MappingProfile));
    }
    private static void AddFluentValidation(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CourseCreateValidation>();
    }
}
