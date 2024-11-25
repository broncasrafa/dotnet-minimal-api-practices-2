using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using Student.API.Endpoints;
using Student.API.Middlewares;
using Student.Infrastructure.Settings;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Student.API.DependencyInjection;

public static class RegisterServices
{
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        app.MapCourseEndpoints();
        app.MapStudentEndpoints();
        app.MapEnrollmentEndpoints();
        app.MapAccountEndpoints();

        return app;
    }

    public static IServiceCollection AddSettingsConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddConfigurationSettings<JWTSettings>(configuration, "JWTSettings");
        //services.AddConfigurationSettings<AzureStorageSettings>(configuration, "AzureStorageSettings");

        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorageSettings"));

        return services;
    }
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

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Informe seu token bearer para acessar os recursos da API da seguinte forma: Bearer {your token here}",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
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
        //.AddJsonOptions(options =>
        //{
        //    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        //});

        services.Configure<JsonOptions>(o => {
            o.SerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        });
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(c =>
        {
            c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"])),
                ValidIssuer = configuration["JWTSettings:Issuer"],
                ValidateIssuer = true,
                ValidateAudience = false
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    await AuthErrorHandler.HandleAuthError(context.HttpContext, StatusCodes.Status401Unauthorized);
                },
                OnForbidden = async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    await AuthErrorHandler.HandleAuthError(context.HttpContext, StatusCodes.Status403Forbidden);
                }
            };
        });
    }

    
}
