using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Student.API.DependencyInjection;

public static class RegisterServices
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllPolicy", policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
        });
        return services;
    }

    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services) 
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Minimal Api Practices 2",
                Version = "v1",
                Description = "Demonstração dos recursos disponíveis na api",
                Contact = new OpenApiContact
                {
                    Name = "Rafael Francisco",
                    Email = "rsfrancisco.applications@gmail.com",
                    Url = new Uri("https://github.com/broncasrafa")
                }
            });


            //Set the comments path for the Swagger JSON and UI.
            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
        });
        return services;
    }

    public static void AddControllerAndJsonConfig(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(o => // Microsoft.AspNetCore.Mvc.NewtonsoftJson
            {
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
    }
}
